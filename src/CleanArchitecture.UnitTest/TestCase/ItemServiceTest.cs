using System.Linq.Expressions;
using AutoMapper;
using ElKood.Application;
using ElKood.Shared.Models;
using ElKood.Application.Services;
using ElKood.Domain.Entities;
using ElKood.Shared.Models.Item;
using Moq;

namespace ElKood.Unittest.TestCase;

public class ItemServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private ItemService _itemService;

    [Fact]
    public async Task ItemService_GetById_ShouldReturnAItem()
    {
        // Arrange
        int itemId = 1;
        var ItemDTO = new ItemDTO
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
        };
        var expect = new Item
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
        };
        _unitOfWorkMock.Setup(u => u.ItemRepository.FirstOrDefaultAsync(b => b.Id == itemId, null))
                       .ReturnsAsync(expect);

        _mapperMock.Setup(m => m.Map<ItemDTO>(expect)).Returns(ItemDTO);
        _itemService = new ItemService(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var actualResult = await _itemService.Get(itemId);

        // Assert
        Assert.Equal(expect.Id, actualResult.Id);
        Assert.Equal(expect.Title, actualResult.Title);
        Assert.Equal(expect.Description, actualResult.Description);
    }

    [Fact]
    public async Task ItemService_Get_ShouldReturnItems()
    {
        // Arrange
        var expectedItems = new List<ItemDTO>
        {
            new ItemDTO
            {
                Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
            },
            new ItemDTO
            {
                Id = 2,
            Title = "Task 1",
            Description = "Task 1 Description",
            }
        };

        var expectedResult = new Pagination<ItemDTO>(expectedItems, expectedItems.Count, 0, 2);

        _unitOfWorkMock
            .Setup(u => u.ItemRepository.ToPagination(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<Item, bool>>>(),
                It.IsAny<Func<IQueryable<Item>, IQueryable<Item>>>(),
                It.IsAny<Expression<Func<Item, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<Expression<Func<Item, ItemDTO>>>()))
            .ReturnsAsync(expectedResult);


        _itemService = new ItemService(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var actualResult = await _itemService.Get(0, 2);

        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(expectedResult.CurrentPage, actualResult.CurrentPage);
        Assert.Equal(expectedResult.TotalPages, actualResult.TotalPages);
        Assert.Equal(expectedResult.PageSize, actualResult.PageSize);
        Assert.Equal(expectedResult.TotalCount, actualResult.TotalCount);
        Assert.Equal(expectedResult.HasPrevious, actualResult.HasPrevious);
        Assert.Equal(expectedResult.HasNext, actualResult.HasNext);
        Assert.Equal(expectedResult.Items?.Count, actualResult.Items?.Count);


        var expect = expectedResult.Items?.ToList();
        var actual = actualResult.Items?.ToList();

        for (int i = 0; i < expectedResult.Items?.Count; i++)
        {
            Assert.Equal(expect?[i].Id, actual?[i].Id);
            Assert.Equal(expect?[i].Title, actual?[i].Title);
            Assert.Equal(expect?[i].Description, actual?[i].Description);
        }
    }

    [Fact]
    public async Task ItemService_Add_ShouldAddItem()
    {
        // Arrange
        var expect = new Item
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
        };

        _mapperMock.Setup(m => m.Map<ItemDTO>(expect)).Returns(new ItemDTO
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
        });

        _unitOfWorkMock.Setup(u => u.ItemRepository.AddAsync(It.IsAny<Item>())).Returns(Task.CompletedTask);

        _itemService = new ItemService(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await _itemService.Add(new AddItemRequest
        {
            Title = "New item",
            Description = "A new item description.",
        }, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.ExecuteTransactionAsync(It.IsAny<Func<Task>>(), CancellationToken.None), Times.Once);
    }


    [Fact]
    public async Task ItemService_Update_ShouldUpdateItem()
    {
        // Arrange
        var updateRequest = new UpdateItemRequest
        {
            Id = 1,
            Title = "Task 1",
            Description = "Task 1 Description",
            Category = "1",
            Priority = 1,
        };

        var itemToUpdate = new Item
        {
            Id = 1,
            Title = "Task 1 test",
            Description = "Task 1 Description test",
            Category = "2",
            Priority = 2,
        };


        _unitOfWorkMock.Setup(u => u.ItemRepository.FirstOrDefaultAsync(
    It.IsAny<Expression<Func<Item, bool>>>(),
    It.IsAny<Func<IQueryable<Item>, IQueryable<Item>>>()))
               .ReturnsAsync(itemToUpdate);

        _itemService = new ItemService(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        await _itemService.Update(updateRequest, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.ItemRepository.FirstOrDefaultAsync(
            It.IsAny<Expression<Func<Item, bool>>>(),
            It.IsAny<Func<IQueryable<Item>, IQueryable<Item>>>()), Times.Once);
    }

    [Fact]
    public async Task ItemService_Delete_ShouldRemoveItem()
    {
        // Arrange
        var itemId = 1;
        var itemToDelete = new Item
        {
            Id = itemId,
            Title = "Item to Delete",
            Description = "A item to delete",
        };

        _unitOfWorkMock.Setup(u => u.ItemRepository.FirstOrDefaultAsync(
            It.IsAny<Expression<Func<Item, bool>>>(),
            It.IsAny<Func<IQueryable<Item>, IQueryable<Item>>>()))
                       .ReturnsAsync(itemToDelete);

        //_unitOfWorkMock.Setup(u => u.ItemRepository.Delete(It.IsAny<item>())).Returns(Task.CompletedTask);

        // Initialize the ItemService with the mocked unit of work
        _itemService = new ItemService(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        await _itemService.Delete(itemId, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.ItemRepository.FirstOrDefaultAsync(
            It.IsAny<Expression<Func<Item, bool>>>(),
            It.IsAny<Func<IQueryable<Item>, IQueryable<Item>>>()), Times.Once);
    }
}
