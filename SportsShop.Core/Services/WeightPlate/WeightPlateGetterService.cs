using Entities.Models;
using RepositoryContracts;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IWeightPlate;

namespace Services
{
    public class WeightPlateGetterService : IWeightPlateGetterService
    {
        private readonly IWeightPlateRepository _weightPlateRepository;
        public WeightPlateGetterService(IWeightPlateRepository weightPlateRepository)
        {
            _weightPlateRepository = weightPlateRepository;
        }

        public async Task<IReadOnlyList<WeightPlateResponse>> GetAllWeightPlates()
        {
            IEnumerable<WeightPlate> weightPlates = await _weightPlateRepository.GetAllWeightPlates();
            return weightPlates.Select(item => item.ToWeightPlateResponse()).ToList();
        }

        public async Task<WeightPlateResponse?> GetWeightPlateById(int id)
        {
            WeightPlate? weightPlate = await _weightPlateRepository.GetWeightPlateById(id);

            if (weightPlate == null)
            {
                return null;
            }

            return weightPlate.ToWeightPlateResponse();
        }
    }
}
