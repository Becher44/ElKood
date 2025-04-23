using ElKood.Infrastructure.Interface;

namespace ElKood.Application;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IItemRepository ItemRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    Task SaveChangesAsync(CancellationToken token);
    Task ExecuteTransactionAsync(Action action, CancellationToken token);
    Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token);
}
