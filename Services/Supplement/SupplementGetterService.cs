using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ISupplement;

namespace Services
{
    public class SupplementGetterService : ISupplementGetterService
    {
        private readonly ISupplementRepository _supplementRepository;
        private readonly ILogger<SupplementGetterService> _logger;

        public SupplementGetterService(ISupplementRepository supplementRepository, ILogger<SupplementGetterService> logger)
        {
            _supplementRepository = supplementRepository;
            _logger = logger;
        }

        public List<SupplementResponse> GetAllSupplements()
        {
            _logger.LogDebug("GetAllSupplements service method");
            return _supplementRepository.GetAllSupplements().Select(item => item.ToSupplementResponse()).ToList();
        }

        public async Task<SupplementResponse> GetSupplementById(int id)
        {
            _logger.LogDebug("GetSupplementById service method. Parameter: id: {id}", id);

            Supplement? supplement = await _supplementRepository.GetSupplementById(id);

            if (supplement == null)
            {
                return null!;
            }

            return supplement.ToSupplementResponse();
        }

        public async Task<List<SupplementResponse>> FilterSupplements(string type, string flavor)
        {
            _logger.LogDebug("FilterSupplements service method. Parameters: type: {type}, flavor: {flavor}", type, flavor);

            IQueryable<SupplementResponse> supplements = _supplementRepository.GetAllSupplements().Select(item => item.ToSupplementResponse());

            if (type != "select")
            {
                supplements = supplements.Where(item => item.Type == type);
            }

            if (flavor != "select")
            {
                supplements = supplements.Where(item => item.Flavor == flavor);
            }

            return await supplements.ToListAsync();
        }
    }
}
