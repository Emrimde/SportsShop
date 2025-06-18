using Entities.Models;
using ServiceContracts.DTO;
using ServiceContracts.DTO.SupplierDto;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.ViewModels
{
    public class CheckoutViewModel
    {
        [Required (ErrorMessage = "Select supplier!")]
        public int? SupplierId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<SupplierResponse> Suppliers { get; set; } = new List<SupplierResponse>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public AddressDTO Address { get; set; } = new AddressDTO();
        public int? AddressId { get; set; }
        public int ItemsPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
