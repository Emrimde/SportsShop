using ServiceContracts.DTO.SupplierDto;

namespace ServiceContracts.Interfaces.ISupplier
{
    public interface ISupplierGetterService
    {
        Task<List<SupplierResponse>> GetAllSuppliers();
        Task<decimal> GetSupplierPriceById(int id);
    }
}
