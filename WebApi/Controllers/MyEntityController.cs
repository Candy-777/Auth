using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEntitiesService;
using MyEntitiesService.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEntityController : ControllerBase
    {
        private readonly IMyEntityService _myEntityService;

        public MyEntityController(IMyEntityService myEntityService)
        {
            _myEntityService = myEntityService;
        }

        [HttpGet("GetEntities")]
        [Authorize]
        public async Task<IActionResult> GetAllEntities() 
        {
            Guid userId = Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok (await _myEntityService.GetMyEntitiesForUserAsync(userId));
        }

        [HttpPost("AddEntities")]
        [Authorize]
        public async Task<IActionResult> AddEntity([FromBody] CreateEntityDto dto)
        {
            Guid userId = Guid.Parse(User.FindFirst("userId")?.Value);
            dto.UserId = userId;
            return Ok(await _myEntityService.AddMyEntityAsync(dto));
        }

        [HttpDelete("DeleteEntities")]
        [Authorize]
        public async Task<IActionResult> DeleteEntities()
        {
            Guid userId = Guid.Parse(User.FindFirst("userId")?.Value);
           ;
            return Ok(await _myEntityService.DeleteAllMyEntitiesAsync(userId));
        }
    }
}
