using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.SupplierDto;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplier;

namespace SportsShop.Core.Services.SupplierServices;
public class SupplierGetterService : ISupplierGetterService
{
    private readonly ISupplierRepository _supplierRepository;
    public SupplierGetterService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<decimal> GetSupplierPriceById(int id)
    {
        return await _supplierRepository.GetSupplierPriceById(id);
    }

    public List<SupplierResponse> GetAllSuppliers()
    {
        return _supplierRepository.GetAllSuppliers().Select(item => item.ToSupplierResponse()).ToList();
    }
}
