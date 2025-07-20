using Entities.Models;

namespace ServiceContracts.DTO.CartItemDto
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
    }
    public static class CartItemExtensions
    {
        public static CartItemResponse ToCartItemResponse(this CartItem cartItem)
        {
            return new CartItemResponse
            {
                Id = cartItem.Id,
                Quantity = cartItem.Quantity,
                Price = cartItem.Product.Price,
                ProductId = cartItem.ProductId,
                Type = cartItem.Type ?? "",
                Producer = cartItem.Product.Producer,
                ProductName = cartItem.Product.Name,
                ProductDescription = cartItem.Product.Description,
                ImagePath = cartItem.Product.ImagePath ?? "",
            };
        }
    }
}
