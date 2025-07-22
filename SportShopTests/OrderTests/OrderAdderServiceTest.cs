using AutoFixture;
using FluentAssertions;
using Moq;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.OrderDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplier;
using SportsShop.Core.Services.OrderServices;

namespace SportShopTests.OrderTests;
public class OrderAdderServiceTest
{
    private readonly IFixture _fixture;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderAdderService _orderAdderService;
    private readonly Mock<ICartGetterService> _cartGetterMock;
    private readonly Mock<ICartDeleterService> _cartDeleterMock;
    private readonly Mock<IAddressGetterService> _addressGetterMock;
    private readonly Mock<IAddressAdderService> _addressAdderMock;
    private readonly Mock<ISupplierGetterService> _supplierGetterMock;

    public OrderAdderServiceTest()
    {
        _fixture = new Fixture();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderRepository = _orderRepositoryMock.Object;
        _cartGetterMock = new Mock<ICartGetterService>();
        _cartDeleterMock = new Mock<ICartDeleterService>();
        _addressGetterMock = new Mock<IAddressGetterService>();
        _supplierGetterMock = new Mock<ISupplierGetterService>();
        _addressAdderMock = new Mock<IAddressAdderService>();

        _orderAdderService = new OrderAdderService(_orderRepository, _cartGetterMock.Object, _cartDeleterMock.Object, _addressGetterMock.Object, _supplierGetterMock.Object, _addressAdderMock.Object);
    }

    [Fact]
    public async Task AddOrder_ShouldReturnAddedOrder()
    {
        //Arrange
        OrderAddRequest orderAddRequest = _fixture.Create<OrderAddRequest>();
        Order order = orderAddRequest.ToOrder();
        OrderResponse expectedOrderResponse = order.ToOrderResponse();
        _orderRepositoryMock.Setup(item => item.AddOrder(It.IsAny<Order>(), It.IsAny<int>())).ReturnsAsync(order);

        //Act
        OrderResponse result = await _orderAdderService.AddOrder(orderAddRequest,10);
        expectedOrderResponse.Id = result.Id;

        //Assert
        result.Should().BeEquivalentTo(expectedOrderResponse);
    }
}
