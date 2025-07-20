using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ISupplement;

namespace Services
{
    public class SupplementGetterService : ISupplementGetterService
    {
        private readonly ISupplementRepository _supplementRepository;
        public SupplementGetterService(ISupplementRepository supplementRepository)
        {
            _supplementRepository = supplementRepository;
        }

        public List<SupplementResponse> GetAllSupplements()
        {
            return _supplementRepository.GetAllSupplements().Select(item => item.ToSupplementResponse()).ToList();
        }

        public async Task<SupplementResponse> GetSupplementById(int id)
        {
            Supplement? supplement = await _supplementRepository.GetSupplementById(id);

            if (supplement == null)
            {
                return null!;
            }

            return supplement.ToSupplementResponse();
        }

        public async Task<List<SupplementResponse>> FilterSupplements(string type, string flavor)
        {
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
