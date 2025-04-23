using ElKood.Application.Common.Interfaces;

namespace ElKood.Application.Services;

public class CurrentTime : ICurrentTime
{
    public DateTime GetCurrentTime() => DateTime.UtcNow;
}
