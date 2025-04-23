using ElKood.Application.Common.Interfaces;
using ElKood.Shared.Models;
using ElKood.Shared.Models.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElKood.Web.Controller;

public class ItemController(IItemService itemService) : BaseController
{
    private readonly IItemService _itemService = itemService;

    /// <summary>
    /// get a item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [SwaggerResponse(200, "Item details retrieved successfully.", typeof(ItemDTO))]
    [SwaggerResponse(404, "Item not found.")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _itemService.Get(id));

    /// <summary>
    /// get a list of items
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [SwaggerResponse(200, "Items retrieved successfully.", typeof(Pagination<ItemDTO>))]
    public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10, string? search = null,
    string? category = null,
    int? priority = null,
    string? sortBy = "Title",
    bool ascending = true)
        => Ok(await _itemService.Get(pageIndex, pageSize, search, category, priority, sortBy, ascending));

    /// <summary>
    /// add a item
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerResponse(201, "Item added successfully.", typeof(ItemDTO))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> Add(AddItemRequest request, CancellationToken token)
        => Ok(await _itemService.Add(request, token));

    /// <summary>
    /// update a item
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut]
    [SwaggerResponse(200, "Item updated successfully.", typeof(ItemDTO))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "Item not found.")]
    public async Task<IActionResult> Update(UpdateItemRequest request, CancellationToken token)
        => Ok(await _itemService.Update(request, token));

    /// <summary>
    /// update task status
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("UpdateStatus")]
    [SwaggerResponse(200, "Item Status updated successfully.", typeof(ItemDTO))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(404, "Item not found.")]
    public async Task<IActionResult> UpdateTaskStatus(UpdateItemStatusRequest request, CancellationToken token)
        => Ok(await _itemService.UpdateTaskStatus(request, token));

    /// <summary>
    /// delete a item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    [SwaggerResponse(200, "Item deleted successfully.")]
    [SwaggerResponse(404, "Item not found.")]
    public async Task<IActionResult> Delete(int id, CancellationToken token)
        => Ok(await _itemService.Delete(id, token));
}
