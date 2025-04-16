using Entities.Models;

namespace SportsShop.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public int ItemsPrice { get; set; }
        public decimal ShippingCost { get; set; }
      
        public decimal TotalCost { get; set; }
    }
}
