using ElKood.IntegrationTest.Config;
using ElKood.IntegrationTest.Shared.Client;
using ElKood.IntegrationTest.Shared.Interface;
using ElKood.Unittest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Extensions.AssemblyFixture;

[assembly: TestFramework(AssemblyFixtureFramework.TypeName, AssemblyFixtureFramework.AssemblyName), CollectionBehavior(MaxParallelThreads = 50)]
namespace ElKood.IntegrationTest;
public class InjectionFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; private set; }

    public InjectionFixture()
    {
        var services = new ServiceCollection();

        // Configuration setup
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = configuration.Get<AppSetting>() ?? throw new Exception("Cannot get appsetting");

        // Register configuration and app settings
        services.AddSingleton(appSettings);

        // Configure HttpClient
        services.AddHttpClient("TestClient", client =>
        {
            client.BaseAddress = new Uri(appSettings.AppUrl);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", appSettings.Token);
        });

        ServiceProvider = new AddServiceCollection()._serviceProvider;
        // Register HttpClient and IItemClient
        services.AddHttpClient<IItemClient, ItemClient>(client =>
        {
            client.BaseAddress = new Uri(appSettings.AppUrl);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", appSettings.Token);
        });
        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public HttpClient GetHttpClient()
    {
        return ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("TestClient");
    }
}
