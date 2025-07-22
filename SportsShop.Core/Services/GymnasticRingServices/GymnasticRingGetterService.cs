using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.GymnasticRingDto;
using SportsShop.Core.ServiceContracts.Interfaces.IGymnasticRing;

namespace SportsShop.Core.Services.GymnasticRingServices;
public class GymnasticRingGetterService : IGymnasticRingGetterService
{
    private readonly IGymnasticRingRepository _gymnasticRingRepository;
    public GymnasticRingGetterService(IGymnasticRingRepository gymnasticRingRepository)
    {
        _gymnasticRingRepository = gymnasticRingRepository;
    }

    public async Task<IReadOnlyList<GymnasticRingResponse>> GetAllGymnasticRings()
    {
        IEnumerable<GymnasticRing> gymnasticRings = await _gymnasticRingRepository.GetAllGymnasticRings();
            
        return gymnasticRings.Select(item => item.ToGymnasticResponse()).ToList();
    }

    public async Task<GymnasticRingResponse?> GetGymnasticRingById(int id)
    {
        GymnasticRing? gymnasticRing = await _gymnasticRingRepository.GetGymnasticRingById(id);

        if (gymnasticRing == null)
        {
            return null!;
        }

        return gymnasticRing.ToGymnasticResponse();
    }
}
