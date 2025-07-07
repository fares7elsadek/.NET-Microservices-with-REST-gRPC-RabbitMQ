using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles;

public class CommandsProfile:Profile
{
    public CommandsProfile()
    {
        CreateMap<Commands, CommandReadDto>();
        CreateMap<CommandCreateDto, Commands>();
        CreateMap<Platforms, PlatformReadDto>();
        CreateMap<PlatformPublishedDto, Platforms>()
            .ForMember(x => x.ExternalId, opt => opt.MapFrom(x => x.Id));
    }
}