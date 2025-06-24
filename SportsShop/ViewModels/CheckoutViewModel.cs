using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CartItemDto;
using ServiceContracts.DTO.SupplierDto;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.ViewModels
{
    public class CheckoutViewModel
    {
        [Required (ErrorMessage = "Select supplier!")]
        public int? SupplierId { get; set; }
        public List<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        public List<SupplierResponse> Suppliers { get; set; } = new List<SupplierResponse>();
        public List<AddressResponse> Addresses { get; set; } = new List<AddressResponse>();
        public AddressAddRequest Address { get; set; } = new AddressAddRequest();
        public int? AddressId { get; set; }
        public int ItemsPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
