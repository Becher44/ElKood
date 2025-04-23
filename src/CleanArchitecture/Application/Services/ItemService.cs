using System.Linq.Expressions;
using AutoMapper;
using ElKood.Application.Common.Exceptions;
using ElKood.Application.Common.Interfaces;
using ElKood.Shared.Models;
using ElKood.Shared.Models.Item;

namespace ElKood.Application.Services;

public class ItemService(IUnitOfWork unitOfWork, IMapper mapper) : IItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Pagination<ItemDTO>> Get(
int pageIndex,
int pageSize,
string? search = null,
string? category = null,
int? priority = null,
string? sortBy = "Title",
bool ascending = true)
    {

        Expression<Func<Item, bool>> filter = x =>
            (string.IsNullOrEmpty(search) || x.Title.Contains(search) || x.Description.Contains(search)) &&
            (string.IsNullOrEmpty(category) || x.Category == category) &&
            (string.IsNullOrEmpty(priority.ToString()) || x.Priority == priority);

        Func<IQueryable<Item>, IOrderedQueryable<Item>> orderBy = query =>
        {
            return sortBy?.ToLower() switch
            {
                "category" => ascending ? query.OrderBy(x => x.Category) : query.OrderByDescending(x => x.Category),
                "priority" => ascending ? query.OrderBy(x => x.Priority) : query.OrderByDescending(x => x.Priority),
                _ => ascending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title)
            };
        };

        var items = await _unitOfWork.ItemRepository.ToPagination(
            pageIndex: pageIndex,
            pageSize: pageSize,
            filter: filter,
            orderBy: x => x.Title,
            selector: x => new ItemDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Category = x.Category,
                Priority = x.Priority,
                IsCompleted = x.IsCompleted,
            });

        return items;
    }



    public async Task<ItemDTO> Get(int id)
    {
        var item = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<ItemDTO>(item);
    }

    public async Task<ItemDTO> Add(AddItemRequest request, CancellationToken token)
    {
        var item = _mapper.Map<Item>(request);
        await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.ItemRepository.AddAsync(item), token);
        return _mapper.Map<ItemDTO>(item);
    }

    public async Task<ItemDTO> Update(UpdateItemRequest request, CancellationToken token)
    {
        var item = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new UserFriendlyException("Item not found", "Item not found");

        var mappedItem = _mapper.Map<Item>(request);
        await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.ItemRepository.UpdateAsync(mappedItem), token);
        return _mapper.Map<ItemDTO>(mappedItem);
    }

    public async Task<ItemDTO> UpdateTaskStatus(UpdateItemStatusRequest request, CancellationToken token)
    {
        var item = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (item == null)
            throw new UserFriendlyException("Item not found", "Item not found");

        item.IsCompleted = request.Status;
        await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.ItemRepository.UpdateAsync(item), token);
        return _mapper.Map<ItemDTO>(item);
    }

    public async Task<ItemDTO> Delete(int id, CancellationToken token)
    {

        var existItem = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new UserFriendlyException("Item not found", "Item not found");

        await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.ItemRepository.Delete(existItem.Id), token);
        return _mapper.Map<ItemDTO>(existItem);
    }
}
