using Entities.Models;

namespace ServiceContracts.DTO.CartItemDto
{
    public class CartItemAddRequest
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; } = default!;
        
        public CartItem ToCartItem()
        {
            return new CartItem()
            {
                Quantity = Quantity,
                Price = Price,
                ProductId = ProductId,
                Type = Type,
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Quantity)}={Quantity.ToString()}, {nameof(Price)}={Price.ToString()}, {nameof(ProductId)}={ProductId.ToString()}, {nameof(Type)}={Type}}}";
        }
    }
}
