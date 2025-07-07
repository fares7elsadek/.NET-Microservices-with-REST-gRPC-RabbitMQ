using AutoMapper;
using CommandsService.Data.Repositories;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController(IMapper mapper,ICommandRepository commandRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(string platformId)
    {
        if (!commandRepository.PlaformExits(platformId))
            return NotFound();
        
        var commands = commandRepository.GetCommandsForPlatform(platformId);
        return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }
    
    [HttpGet("{commandId}",Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(string platformId,string commandId)
    {
        if (!commandRepository.PlaformExits(platformId))
            return NotFound();
        
        var command = commandRepository.GetCommand(platformId, commandId);
        return Ok(mapper.Map<CommandReadDto>(command));
    }
    
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(string platformId,CommandCreateDto commandCreateDto)
    {
        if (!commandRepository.PlaformExits(platformId))
            return NotFound();
        
        var command = mapper.Map<Commands>(commandCreateDto);
        commandRepository.CreateCommand(platformId, command);
        commandRepository.SaveChanges();
        return CreatedAtRoute(nameof(GetCommandForPlatform),new { platformId, commandId = command.Id }, mapper.Map<CommandReadDto>(command));
    }
    
}