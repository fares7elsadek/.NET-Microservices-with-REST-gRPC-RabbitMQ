namespace CommandsService.Models;

public class Commands
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string HowTo { get; set; } = default!;
    public string CommandLine { get; set; } = default!;
    public string PlatformId { get; set; } = default!;
    public Platforms Platform { get; set; } = default!;
}