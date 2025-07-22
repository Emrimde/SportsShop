using SportsShop.Core.ServiceContracts.DTO.WeightPlateDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IWeightPlate;
public interface IWeightPlateGetterService
{
    Task<IReadOnlyList<WeightPlateResponse>> GetAllWeightPlates();
    Task<WeightPlateResponse?> GetWeightPlateById(int id);
}
