using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.SupplierDto;
using ServiceContracts.Interfaces.ISupplier;

namespace Services
{
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

        public async Task<List<SupplierResponse>> GetAllSuppliers()
        {
            return await _supplierRepository.GetAllSuppliers().Select(item => item.ToSupplierResponse()).ToListAsync();
        }
    }
}
