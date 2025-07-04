using ServiceContracts.DTO.SupplierDto;

namespace ServiceContracts.Interfaces.ISupplier
{
    public interface ISupplierGetterService
    {
        List<SupplierResponse> GetAllSuppliers();
        Task<decimal> GetSupplierPriceById(int id);
    }
}
