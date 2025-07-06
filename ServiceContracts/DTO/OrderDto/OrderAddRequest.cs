using Entities.Models;
using ServiceContracts.DTO.CartItemDto;

namespace ServiceContracts.DTO.OrderDto
{
    public class OrderAddRequest
    {
        public List<CartItemAddRequest> CartItems { get; set; } = new List<CartItemAddRequest>();
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int AddressId { get; set; }
        public int SupplierId { get; set; }
        public Guid UserId { get; set; }
        public string? Coupon { get; set; } = "";
        public bool IsPaid { get; set; } = true;

        public Order ToOrder()
        {
            return new Order()
            {
                CartItems = CartItems.Select(item => item.ToCartItem()).ToList(),
                TotalCost = TotalCost,
                ShippingCost = ShippingCost,
                OrderDate = OrderDate,
                Coupon = Coupon,
                AddressId = AddressId,
                SupplierId = SupplierId,
                UserId = UserId,
                IsPaid = IsPaid,
            };
        }
    }
}
