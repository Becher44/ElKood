namespace ElKood.Application.Common.Interfaces;

public interface ICurrentUser
{
    public int GetCurrentUserId();
    public string GetCurrentStringUserId();
}
