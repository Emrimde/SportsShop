using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<WeightPlateResponse>> GetAllWeightPlates()
        {
            return await _weightPlateRepository.GetAllWeightPlates()
                .Select(item => item.ToWeightPlateResponse())
                .ToListAsync();
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
