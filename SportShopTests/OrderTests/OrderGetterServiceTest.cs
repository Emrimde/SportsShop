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
        public void GetAllOrders_ShouldReturnAllOrder()
        {
            //Arrange
            List<Order> order = new List<Order>
            {
                _fixture.Create<Order>(),
                _fixture.Create<Order>(),
                _fixture.Create<Order>()
            };
            
            List<OrderResponse> expected = order.Select(item => item.ToOrderResponse()).ToList();
            _orderRepositoryMock.Setup(item => item.GetAllOrders(It.IsAny<string>())).Returns(order.AsQueryable());
            string goodUserId = "goodUserId";

            //Act
            List<OrderResponse> result = _orderGetterService.GetAllOrders(goodUserId);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAllOrders_ShouldReturnEmpty()
        {
            //Arrange
            _orderRepositoryMock.Setup(item => item.GetAllOrders(It.IsAny<string>())).Returns(new List<Order>().AsQueryable());
            string goodUserId = "goodUserId";

            //Act
            List<OrderResponse> result = _orderGetterService.GetAllOrders(goodUserId);

            //Assert
            result.Should().BeEmpty();
        }

        #endregion
    }
}
