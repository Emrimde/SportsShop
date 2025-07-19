using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.IOrder;
using ServiceContracts.Interfaces.ISupplier;
using Services;

namespace SportShopTests.OrderTests
{  
    public class OrderAdderServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICartGetterService> _cartGetterMock;
        private readonly Mock<ICartDeleterService> _cartDeleterMock;
        private readonly Mock<IAddressGetterService> _addressGetterMock;
        private readonly Mock<IAddressAdderService> _addressAdderMock;
        private readonly Mock<ISupplierGetterService> _supplierGetterMock;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderAdderService _orderAdderService;

        public OrderAdderServiceTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepository = _orderRepositoryMock.Object;
            _orderAdderService = new OrderAdderService(_orderRepository,_cartGetterMock.Object,_cartDeleterMock.Object,_addressGetterMock.Object,_supplierGetterMock.Object,_addressAdderMock.Object);
            _fixture = new Fixture();
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
            OrderResponse result = await _orderAdderService.AddOrder(orderAddRequest, 10);
            expectedOrderResponse.Id = result.Id;

            //Assert
            result.Should().BeEquivalentTo(expectedOrderResponse);
        }
    }
}
