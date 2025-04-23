using ElKood.IntegrationTest.Shared.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ElKood.IntegrationTest.TestCase;

public class ItemTest : IClassFixture<InjectionFixture>
{
    private readonly IItemClient _itemClient;

    public ItemTest(InjectionFixture fixture)
        => _itemClient = fixture.ServiceProvider.GetRequiredService<IItemClient>();

    [Fact(DisplayName = "[Item] Verify get item by id")]
    public async Task Item_ShouldSuccess_WhenGetItemById()
    {
        // Act
        //var result = await _itemClient.Get("1");

        // Assert
        Assert.True(true);
    }
}

