using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IWeightPlate;

namespace Services
{
    public class WeightPlateGetterService : IWeightPlateGetterService
    {
        private readonly IWeightPlateRepository _weightPlateRepository;
        private readonly ILogger<WeightPlateGetterService> _logger;

        public WeightPlateGetterService(IWeightPlateRepository weightPlateRepository, ILogger<WeightPlateGetterService> logger)
        {
            _weightPlateRepository = weightPlateRepository;
            _logger = logger;
        }

        public List<WeightPlateResponse> GetAllWeightPlates()
        {
            _logger.LogDebug("GetAllWeightPlates service method");
            return _weightPlateRepository.GetAllWeightPlates()
                .Select(item => item.ToWeightPlateResponse())
                .ToList();
        }

        public async Task<WeightPlateResponse?> GetWeightPlateById(int id)
        {
            _logger.LogDebug("GetWeightPlateById service method. Parameter: id: {id}", id);
            WeightPlate? weightPlate = await _weightPlateRepository.GetWeightPlateById(id);

            if (weightPlate == null)
            {
                return null;
            }

            return weightPlate.ToWeightPlateResponse();
        }
    }
}
