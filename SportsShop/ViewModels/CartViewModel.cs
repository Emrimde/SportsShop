using ServiceContracts.DTO.CartItemDto;

namespace SportsShop.ViewModels
{
    public class CartViewModel
    {
        public IReadOnlyList<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        public int TotalCost { get; set; }
    }
}
