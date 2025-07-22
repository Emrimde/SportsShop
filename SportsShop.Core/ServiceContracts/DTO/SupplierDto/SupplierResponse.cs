using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.ServiceContracts.DTO.SupplierDto;
public class SupplierResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
public static class SupplierResponseExtensions
{
    public static SupplierResponse ToSupplierResponse(this Supplier supplier)
    {
        return new SupplierResponse
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Price = supplier.Price
        };
    }
}
