using AuthService.DTOs;
using AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {        
        private readonly IAuthService _userService;
        public AuthController(IAuthService userService)
        {
            _userService = userService;
           
        }

        [HttpGet("GetMe")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirst("userId")?.Value;
            return Ok(new
            {
                UserId = userId,                
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegNewUser(RegisterReqiestDto registerReqiestDto)
        {
            return Ok(await _userService.Register(registerReqiestDto));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _userService.Login(loginRequestDto);
            HttpContext.Response.Cookies.Append("MyCookies", result.Token);

            return Ok(result);
        }
    }
}
