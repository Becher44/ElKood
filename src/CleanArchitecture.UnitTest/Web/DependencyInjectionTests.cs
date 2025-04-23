using System.Diagnostics;
using ElKood.Application;
using ElKood.Application.Common;
using ElKood.Application.Common.Interfaces;
using ElKood.Application.Services;
using ElKood.Infrastructure;
using ElKood.Web;
using ElKood.Web.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace ElKood.Unittest.Web;

public class DependencyInjectionTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly AppSettings _appSettings = new AppSettings
    {
        AppUrl = "",
        ApplicationDetail = new ApplicationDetail
        {
            ApplicationName = "app",
            Description = "description"
        },
        ConnectionStrings = new ConnectionStrings
        {
            DefaultConnection = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
        },
        Identity = new Identity
        {
            Key = "your_jwt_key",
            Issuer = "your_jwt_issuer",
            Audience = "your_jwt_audience",
            ExpiredTime = 10
        },
        UseInMemoryDatabase = false,
        Cors =
        [
        "http://localhost:4200",
            "https://myapp.com"
        ],
        BaseURL = "https://myapp.com"
    };

    public DependencyInjectionTests()
    {
        var service = new ServiceCollection();
        service.AddApplicationService(_appSettings);
        service.AddInfrastructuresService(_appSettings);
        service.AddWebAPIService(_appSettings);

        _serviceProvider = service.BuildServiceProvider();
    }

    [Fact]
    public void DependencyInjectionTests_ServiceShouldResolveCorrectly()
    {
        var currentTimeServiceResolved = _serviceProvider.GetRequiredService<ICurrentTime>();
        var exceptionMiddlewareResolved = _serviceProvider.GetRequiredService<GlobalExceptionMiddleware>();
        var performanceMiddleware = _serviceProvider.GetRequiredService<PerformanceMiddleware>();
        var stopwatchResolved = _serviceProvider.GetRequiredService<Stopwatch>();

        Assert.Equal(typeof(CurrentTime), currentTimeServiceResolved.GetType());
        Assert.Equal(typeof(GlobalExceptionMiddleware), exceptionMiddlewareResolved.GetType());
        Assert.Equal(typeof(Stopwatch), stopwatchResolved.GetType());
        Assert.Equal(typeof(PerformanceMiddleware), performanceMiddleware.GetType());
    }
}
