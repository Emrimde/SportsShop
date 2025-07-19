using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.CartItemDto
{
    public class CartItemAddRequest
    {
        [Required]
        [Range(1,4, ErrorMessage =("Wrong quantity number"))]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public string Type { get; set; } = default!;
        public int CartId { get; set; } 
        
        public CartItem ToCartItem()
        {
            return new CartItem()
            {
                Quantity = Quantity,
                ProductId = ProductId,
                Price = Price,
                Type = Type,
                CartId = CartId
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Quantity)}={Quantity.ToString()}, {nameof(ProductId)}={ProductId.ToString()}, {nameof(Type)}={Type}}}";
        }
    }
}
