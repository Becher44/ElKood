using ElKood.Shared.Models;

namespace ElKood.Domain.Entities;

public class Item : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
    public bool IsCompleted { get; set; } = false;
}
