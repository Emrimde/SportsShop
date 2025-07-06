using AutoFixture;
using Entities.Models;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO.OrderDto;
using ServiceContracts.Interfaces.IOrder;
using Services;

namespace SportShopTests.OrderTests
{  
    public class OrderAdderServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderAdderService _orderAdderService;

        public OrderAdderServiceTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepository = _orderRepositoryMock.Object;
            _orderAdderService = new OrderAdderService(_orderRepository);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddOrder_ShouldReturnAddedOrder()
        {
            //Arrange
            OrderAddRequest orderAddRequest = _fixture.Create<OrderAddRequest>();
            Order order = orderAddRequest.ToOrder();
            OrderResponse expectedOrderResponse = order.ToOrderResponse();
            _orderRepositoryMock.Setup(item => item.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);

            //Act
            OrderResponse result = await _orderAdderService.AddOrder(orderAddRequest);

            //Assert
            result.Should().BeEquivalentTo(expectedOrderResponse);
        }
    }
}
