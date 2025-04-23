namespace ElKood.Shared.Models.Item;

public class ItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
    public bool IsCompleted { get; set; } = false;
}

public class AddItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
}
public class UpdateItemRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
}

public class UpdateItemStatusRequest
{
    public int Id { get; set; }
    public bool Status { get; set; }

}
