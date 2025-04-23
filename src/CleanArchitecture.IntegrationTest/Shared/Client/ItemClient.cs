using ElKood.IntegrationTest.Models;
using ElKood.IntegrationTest.Shared.Interface;

namespace ElKood.IntegrationTest.Shared.Client;
public class ItemClient : IItemClient
{
    private readonly HttpClient _client;

    public ItemClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResponse<object, string>> Get(string id)
    {
        var response = await _client.GetAsync($"/{id}");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return new ApiResponse<object, string>
            {
                HttpResponseMessage = response,
                Data = data
            };
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new ApiResponse<object, string>
            {
                HttpResponseMessage = response,
                ErrorData = errorContent
            };
        }
    }

    public async Task<ApiResponse<object, string>> Get(int pageIndex, int pageSize)
    {
        var response = await _client.GetAsync($"/Item?pageIndex={pageIndex}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return new ApiResponse<object, string>
            {
                HttpResponseMessage = response,
                Data = data
            };
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new ApiResponse<object, string>
            {
                HttpResponseMessage = response,
                ErrorData = errorContent
            };
        }
    }
}
