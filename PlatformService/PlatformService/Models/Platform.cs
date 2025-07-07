namespace PlatformService.Models;

public class Platform
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public string Publisher { get; set; } = default!;
    public string Cost { get; set; } = default!;
}
