using AuthService.DTOs;
using CustomExceptions;
using Domain.Entities;
using JwtProvider;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;


namespace AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
      

        public AuthService(UserManager<AppUser> userManager, IJwtProvider jwtProvider) 
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.EmailOrUsername)
                ?? await _userManager.FindByNameAsync(loginRequestDto.EmailOrUsername);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                throw new InvalidCredentialsException();
            }
            var token = await _jwtProvider.GenerateToken(user);
            return new LoginResponseDto {Token = token};

        }

        public async Task<RegisterResponceDto> Register(RegisterReqiestDto registerReqiestDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerReqiestDto.UserName);
            if (userExists != null)
                throw new UserAlreadyExistsException("User with this username already exists");
            var user = new AppUser
            {
                UserName = registerReqiestDto.UserName,
                Email = registerReqiestDto.Email,
            };            
            var result = await _userManager.CreateAsync(user,registerReqiestDto.Password);
            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            
            else
                throw new ArgumentException("Something wrong");
            return new RegisterResponceDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = registerReqiestDto.Password,
                Roles = await _userManager.GetRolesAsync(user)
            };


        }
    }
}
