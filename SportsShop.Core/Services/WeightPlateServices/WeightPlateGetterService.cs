using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.WeightPlateDto;
using SportsShop.Core.ServiceContracts.Interfaces.IWeightPlate;

namespace SportsShop.Core.Services.WeightPlateServices;
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
