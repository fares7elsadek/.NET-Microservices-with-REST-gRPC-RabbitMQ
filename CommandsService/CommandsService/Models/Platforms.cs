using Microsoft.EntityFrameworkCore;

namespace CommandsService.Models;

public class Platforms
{
    public Platforms()
    {
        Commands = new List<Commands>();
    }
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public string ExternalId { get; set; } = default!;
    public ICollection<Commands> Commands { get; set; } 
    
}