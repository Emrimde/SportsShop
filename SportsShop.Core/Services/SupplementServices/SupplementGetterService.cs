using Microsoft.EntityFrameworkCore;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.SupplementDto;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplement;

namespace SportsShop.Core.Services.SupplementServices;
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
