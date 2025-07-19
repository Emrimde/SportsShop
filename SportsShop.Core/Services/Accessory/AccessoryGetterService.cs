using Entities.Models;
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
        public AccessoryGetterService(IAccessoryRepository accessoryRepository)
        {
            _accessoryRepository = accessoryRepository;
        }

        public async Task<List<dynamic>> FilterAccessory(string type)
        {
            List<dynamic> accessories = await _accessoryRepository.FilterAccessory(type);
            if (type == "GymnasticRing")
            {
                return accessories
                    .Cast<GymnasticRing>()
                    .Select(item => item.ToGymnasticResponse())
                    .Cast<dynamic>()
                    .ToList();
            }

            if (type == "TrainingRubber")
            {
                return accessories
                    .Cast<TrainingRubber>()
                    .Select(item => item.ToTrainingRubberResponse())
                    .Cast<dynamic>()
                    .ToList();
            }
            if (type == "WeightPlate")
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
