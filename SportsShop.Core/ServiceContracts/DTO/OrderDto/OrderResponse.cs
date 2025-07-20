using Entities.Models;
using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.DTO.OrderDto
{
    public class OrderResponse
    {
        public int Id { get; set; } 
        public List<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Coupon { get; set; }
        public bool IsPaid { get; set; }
    }
    public static class CartItemExtensions
    {
        public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                CartItems = order.CartItems.Select(item => item.ToCartItemResponse()).ToList(),
                TotalCost = order.TotalCost,
                ShippingCost = order.ShippingCost,
                OrderDate = order.OrderDate,
                Coupon = order.Coupon,
                IsPaid = order.IsPaid,
            };
        }
    }
}