using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;

namespace ElKood.Application.Repositories;

public class RefreshTokenRepository(ApplicationDbContext context) : GenericRepository<RefreshToken>(context), IRefreshTokenRepository { }
