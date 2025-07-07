using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.Interfaces.IGymnasticRing;

namespace Services
{
    public class GymnasticRingGetterService : IGymnasticRingGetterService
    {
        private readonly IGymnasticRingRepository _gymnasticRingRepository;
        private readonly ILogger<GymnasticRingGetterService> _logger;

        public GymnasticRingGetterService(IGymnasticRingRepository gymnasticRingRepository, ILogger<GymnasticRingGetterService> logger)
        {
            _gymnasticRingRepository = gymnasticRingRepository;
            _logger = logger;
        }

        public List<GymnasticRingResponse> GetAllGymnasticRings()
        {
            _logger.LogDebug("GetAllGymnasticRings service method");
            return _gymnasticRingRepository.GetAllGymnasticRings().Select(item => item.ToGymnasticResponse()).ToList();
        }

        public async Task<GymnasticRingResponse?> GetGymnasticRingById(int id)
        {
            _logger.LogDebug("GetGymnasticRingById service method. Parameter: id: {id}", id);

            GymnasticRing? gymnasticRing = await _gymnasticRingRepository.GetGymnasticRingById(id);

            if (gymnasticRing == null)
            {
                return null!;
            }

            return gymnasticRing.ToGymnasticResponse();
        }
    }
}
