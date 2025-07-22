using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.ServiceContracts.DTO.SupplementDto;
public class SupplementResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Flavor { get; set; } = default!;
    public string Weight { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
}

public static class SupplementResponseExtensions
{
    public static SupplementResponse ToSupplementResponse(this Supplement supplement)
    {
        return new SupplementResponse() {
            Id = supplement.Product.Id,
            Name = supplement.Product.Name,
            Description = supplement.Product.Description,
            Price = supplement.Product.Price,
            Quantity = supplement.Product.Quantity,
            Flavor = supplement.Flavor,
            Weight = supplement.Weight,
            Type = supplement.Type,
            ImagePath = supplement.ImagePath,
        };
    }
}
