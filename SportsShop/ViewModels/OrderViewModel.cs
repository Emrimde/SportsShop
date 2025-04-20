using Entities.Models;

namespace SportsShop.ViewModels
{
    public class OrderViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Coupon { get; set; }
        public bool IsPaid { get; set; }
    }
}
