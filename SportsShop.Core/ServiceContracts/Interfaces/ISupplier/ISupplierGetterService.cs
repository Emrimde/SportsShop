using SportsShop.Core.ServiceContracts.DTO.SupplierDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ISupplier;
public interface ISupplierGetterService
{
    List<SupplierResponse> GetAllSuppliers();
    Task<decimal> GetSupplierPriceById(int id);
}
