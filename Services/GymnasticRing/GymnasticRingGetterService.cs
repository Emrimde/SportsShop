using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.Interfaces.IGymnasticRing;

namespace Services
{
    public class GymnasticRingGetterService : IGymnasticRingGetterService
    {
        private readonly IGymnasticRingRepository _gymnasticRingRepository;

        public GymnasticRingGetterService(IGymnasticRingRepository gymnasticRingRepository)
        {
            _gymnasticRingRepository = gymnasticRingRepository;
        }

        public async Task<List<GymnasticRingResponse>> GetAllGymnasticRings()
        {
            return await _gymnasticRingRepository.GetAllGymnasticRings().Select(item => item.ToGymnasticResponse()).ToListAsync();
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
}
