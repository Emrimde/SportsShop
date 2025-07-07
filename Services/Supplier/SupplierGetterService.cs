using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.SupplierDto;
using ServiceContracts.Interfaces.ISupplier;

namespace Services
{
    public class SupplierGetterService : ISupplierGetterService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<SupplierGetterService> _logger;

        public SupplierGetterService(ISupplierRepository supplierRepository, ILogger<SupplierGetterService> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public async Task<decimal> GetSupplierPriceById(int id)
        {
            _logger.LogDebug("GetSupplierPriceById service method. Parameter: id: {id}", id);
            return await _supplierRepository.GetSupplierPriceById(id);
        }

        public List<SupplierResponse> GetAllSuppliers()
        {
            _logger.LogDebug("GetAllSuppliers service method");

            return _supplierRepository.GetAllSuppliers().Select(item => item.ToSupplierResponse()).ToList();
        }
    }
}
