using ElKood.Shared.Models;
using ElKood.Shared.Models.Item;

namespace ElKood.Application.Common.Interfaces;

public interface IItemService
{
    Task<Pagination<ItemDTO>> Get(
    int pageIndex,
    int pageSize,
    string? search = null,
    string? category = null,
    int? priority = null,
    string? sortBy = "Title",
    bool ascending = true);
    Task<ItemDTO> Get(int id);
    Task<ItemDTO> Add(AddItemRequest request, CancellationToken token);
    Task<ItemDTO> Update(UpdateItemRequest request, CancellationToken token);
    Task<ItemDTO> UpdateTaskStatus(UpdateItemStatusRequest request, CancellationToken token);
    Task<ItemDTO> Delete(int id, CancellationToken token);
}
