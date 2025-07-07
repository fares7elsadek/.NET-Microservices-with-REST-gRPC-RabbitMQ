using PlatformService.Dtos;

namespace PlatformService.Services;

public interface IPlatformService
{
    public Task<IEnumerable<PlatformReadDto>> GetAllAsync();
    public Task<PlatformReadDto?> GetAsync(string id);
    public Task CreateAsync(PlatformCreateDto platformCreateDto);
}
