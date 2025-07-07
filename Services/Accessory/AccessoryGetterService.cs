using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IAccessory;

namespace Services.Accessory
{
    public class AccessoryGetterService : IAccessoryGetterService
    {
        private readonly IAccessoryRepository _accessoryRepository;
        private readonly ILogger<AccessoryGetterService> _logger;
        public AccessoryGetterService(IAccessoryRepository accessoryRepository, ILogger<AccessoryGetterService> logger)
        {
            _accessoryRepository = accessoryRepository;
            _logger = logger;
        }

        public async Task<List<dynamic>> FilterAccessory(string type)
        {
            _logger.LogDebug("FilterAccessory method. Parameter: type: {type}", type);
           
            List<dynamic> accessories = await _accessoryRepository.FilterAccessory(type);
            if (type == "GymnasticRings")
            {
                return accessories
                    .Cast<GymnasticRing>()
                    .Select(item => item.ToGymnasticResponse())
                    .Cast<dynamic>()
                    .ToList();
            }

            if (type == "RubberBand")
            {
                return accessories
                    .Cast<TrainingRubber>()
                    .Select(item => item.ToTrainingRubberResponse())
                    .Cast<dynamic>()
                    .ToList();
            }
            if (type == "Weights")
            {
                return accessories
                    .Cast<WeightPlate>()
                    .Select(item => item.ToWeightPlateResponse())
                    .Cast<dynamic>()
                    .ToList();
            }
            return new List<dynamic>();
        }
    }
}
