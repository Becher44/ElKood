using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;

namespace ElKood.Application.Repositories;

public class ItemRepository(ApplicationDbContext context) : GenericRepository<Item>(context), IItemRepository
{
}
