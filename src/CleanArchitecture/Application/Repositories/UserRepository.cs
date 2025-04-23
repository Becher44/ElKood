using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;

namespace ElKood.Application.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository { }
