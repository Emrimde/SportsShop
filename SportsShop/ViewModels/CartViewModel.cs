using SportsShop.Core.ServiceContracts.DTO.CartItemDto;

namespace SportsShop.UI.ViewModels;
public class CartViewModel
{
    public IReadOnlyList<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
    public int TotalCost { get; set; }
}
