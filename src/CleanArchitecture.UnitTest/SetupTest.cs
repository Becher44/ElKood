using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoMapper;
using ElKood.Application;
using ElKood.Application.Common.Interfaces;
using ElKood.Application.Common.Mappings;
using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ElKood.Unittest;

[ExcludeFromCodeCoverage]
public class SetupTest : IDisposable
{

    protected readonly IMapper _mapperConfig;
    protected readonly Fixture _fixture;
    protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
    protected readonly ApplicationDbContext _dbContext;
    protected readonly Mock<IItemService> _itemServiceMock;
    protected readonly Mock<ICurrentTime> _currentTimeMock;
    protected readonly Mock<IItemRepository> _ItemRepositoryMock;
    protected readonly Mock<IUserRepository> _userRepository;

    public SetupTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapProfile());
        });
        _mapperConfig = mappingConfig.CreateMapper();
        _fixture = new Fixture();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _itemServiceMock = new Mock<IItemService>();
        _currentTimeMock = new Mock<ICurrentTime>();
        _ItemRepositoryMock = new Mock<IItemRepository>();
        _userRepository = new Mock<IUserRepository>();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ApplicationDbContext(options);

        _currentTimeMock.Setup(x => x.GetCurrentTime()).Returns(DateTime.UtcNow);
    }
    public void Dispose() => _dbContext.Dispose();
}
