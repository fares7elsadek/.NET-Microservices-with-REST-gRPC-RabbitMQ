using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;
using PlatformService.Services;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController(IPlatformService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllAsync()
        {
            return Ok(await service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformReadDto>> GetAsync(string id)
        {
            var platform = await service.GetAsync(id);
            return platform!=null ? Ok(platform) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PlatformCreateDto platformDto)
        {
            await service.CreateAsync(platformDto);
            return Ok();
        }


    }
}
