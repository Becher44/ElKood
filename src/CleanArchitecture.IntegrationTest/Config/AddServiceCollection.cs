using ElKood.IntegrationTest.Shared.Client;
using ElKood.IntegrationTest.Shared.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ElKood.IntegrationTest.Config;

public class AddServiceCollection
{
    public readonly IServiceProvider _serviceProvider;
    public AddServiceCollection()
    {
        var serviceCollection = new ServiceCollection();
        // register service here
        serviceCollection.AddTransient<IItemClient, ItemClient>();

        _serviceProvider = serviceCollection.BuildServiceProvider();

    }
}
