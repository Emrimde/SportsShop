using AutoFixture;
using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;
using SportsShop.Core.Services.OrderServices;

namespace SportShopTests.OrderTests;
public class OrderGetterServiceTest
{
    private readonly IFixture _fixture;
    private readonly IOrderRepository _orderRepository;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly IOrderGetterService _orderGetterService;
    
    public OrderGetterServiceTest()
    {
        _fixture = new Fixture();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderRepository = _orderRepositoryMock.Object;
        _orderGetterService = new OrderGetterService(_orderRepository);

        _fixture.Behaviors
        .OfType<ThrowingRecursionBehavior>()
        .ToList()
        .ForEach(item => _fixture.Behaviors.Remove(item));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    #region GetAllOrders

    [Fact]
    public async Task GetAllOrders_ShouldReturnAllOrder()
    {
        //Arrange
        List<Order> order = new List<Order>
        {
            _fixture.Create<Order>(),
            _fixture.Create<Order>(),
            _fixture.Create<Order>()
        };
        
        List<OrderResponse> expected = order.Select(item => item.ToOrderResponse()).ToList();
        _orderRepositoryMock.Setup(item => item.GetAllOrders(It.IsAny<Guid>())).ReturnsAsync(order);
        Guid guid = Guid.NewGuid();

        //Act
        IEnumerable<OrderResponse> result = await _orderGetterService.GetAllOrders(guid);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnEmpty()
    {
        //Arrange
        _orderRepositoryMock.Setup(item => item.GetAllOrders(It.IsAny<Guid>())).ReturnsAsync(new List<Order>());
        Guid guid = Guid.NewGuid();

        //Act
        IEnumerable<OrderResponse> result = await _orderGetterService.GetAllOrders(guid); 

        //Assert
        result.Should().BeEmpty();
    }

    #endregion
}
