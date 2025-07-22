using Microsoft.AspNetCore.Mvc.Rendering;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.DTO.CartItemDto;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.UI.ViewModels;
    public class CheckoutViewModel
    {
        [Required (ErrorMessage = "Select supplier!")]
        public int? SupplierId { get; set; }
        public IEnumerable<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        //public IEnumerable<SupplierResponse> Suppliers { get; set; } = new List<SupplierResponse>();
        public List<SelectListItem> Supplierss { get; set; } = new List<SelectListItem>();
        public IReadOnlyList<AddressResponse> Addresses { get; set; } = new List<AddressResponse>();
        public AddressAddRequest Address { get; set; } = new AddressAddRequest();
        public int? AddressId { get; set; }
        public int ItemsPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; } 
    }

    

