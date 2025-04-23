namespace ElKood.Shared.Models.AuthIdentity.UsersIdentity;

public class UserViewModel
{
    public Guid? Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public Status? Status { get; set; }
    public List<string>? Roles { get; set; }
}
