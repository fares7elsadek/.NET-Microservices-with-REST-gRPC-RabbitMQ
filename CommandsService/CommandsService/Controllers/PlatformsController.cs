using AutoMapper;
using CommandsService.Data.Repositories;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController(IMapper mapper,ICommandRepository commandRepository) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("Getting platforms");
            return Ok(mapper.
                Map<IEnumerable<PlatformReadDto>>(commandRepository.GetAllPlatforms()));
        }
        
        [HttpPost]
        public ActionResult TestInBoundConntection()
        {
            Console.WriteLine("Test in bound connection sccussfully established");
            return Ok("Test in bound connection sccussfully established");
        }
    }
}
