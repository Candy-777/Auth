using AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _adminService.GetAllUsers());
        }

        [HttpGet("GetUserByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            return Ok(await _adminService.GetUserByName(username));
        }

        [HttpPost("DeleteUserByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserByName(string username)
        {
            return Ok(await _adminService.DeleteUserByUserName(username));
        }
    }
}
