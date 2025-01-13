using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtProvider
{
    public class JwtProvider : IJwtProvider
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOptions _options;

        public JwtProvider(UserManager<AppUser> userManager, IOptions<JwtOptions> options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.SecretKey))
                throw new ArgumentException("SecretKey cannot be null or empty in JwtOptions");
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new("userId", user.Id.ToString()),             

            };
            foreach (var r in await _userManager.GetRolesAsync(user))
            {
                var claim = new Claim(ClaimTypes.Role, r);
                claims.Add(claim);
                
            }
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;

        }
    }
}
