using ElKood.IntegrationTest.Models;

namespace ElKood.IntegrationTest.Shared.Interface;

public interface IItemClient
{
    Task<ApiResponse<object, string>> Get(string id);
    Task<ApiResponse<object, string>> Get(int pageIndex, int pageSize);
}
