using ElKood.Application.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ElKood.Infrastructure.Data;
/// <summary>
/// Another way to seed data use DbContext
/// </summary>
public class ApplicationDbContextInitializer(ApplicationDbContext context, ILoggerFactory logger)
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger _logger = logger.CreateLogger<ApplicationDbContextInitializer>();

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
            await SeedUser();
            await SeedItem();
        }
        catch (Exception exception)
        {
            _logger.LogError("Migration error {exception}", exception);
            throw;
        }
    }

    public async Task SeedUser()
    {
        if (!await _context.Users.AnyAsync())
        {
            await _context.Users.AddRangeAsync(
        new List<User>{
                new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "P@ssw0rd".Hash(),
                    Role = Role.Admin
                },
                new User
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    Password = "P@ssw0rd".Hash(),
                    Role = Role.User
                },
            }
        );
            await _context.SaveChangesAsync();
        }

    }

    public async Task SeedItem()
    {
        if (!await _context.Items.AnyAsync())
        {
            await _context.Items.AddRangeAsync(
        new List<Item> {  new Item
    {
        Title = "Task 1",
        Description = "Task 1 Description",
    },
    new Item
    {
        Title = "Task 2",
        Description = "Task 2 Description",
    },
    new Item
    {
        Title = "Task 3",
        Description = "Task 3 Description",
    },
    new Item
    {
        Title = "Task 4",
        Description = "Task 4 Description",
    },
    new Item
    {
        Title = "Task 5",
        Description = "Task 5 Description",
    } }
        );

            await _context.SaveChangesAsync();
        }
    }
}
