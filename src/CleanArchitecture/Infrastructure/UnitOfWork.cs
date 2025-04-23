using ElKood.Application;
using ElKood.Application.Common.Exceptions;
using ElKood.Application.Repositories;
using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace ElKood.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository UserRepository { get; }
    public IItemRepository ItemRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        UserRepository = new UserRepository(_context);
        ItemRepository = new ItemRepository(_context);
        RefreshTokenRepository = new RefreshTokenRepository(_context);
    }
    public async Task SaveChangesAsync(CancellationToken token)
        => await _context.SaveChangesAsync(token);

    public async Task ExecuteTransactionAsync(Action action, CancellationToken token)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(token);
        try
        {
            action();
            await _context.SaveChangesAsync(token);
            await transaction.CommitAsync(token);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(token);
            throw TransactionException.TransactionNotExecuteException(ex);
        }
    }

    public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(token);

            try
            {
                await action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                throw TransactionException.TransactionNotExecuteException(ex);
            }
        });
    }
}
