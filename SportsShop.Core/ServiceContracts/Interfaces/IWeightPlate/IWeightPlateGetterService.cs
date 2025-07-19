using ServiceContracts.DTO.WeightPlateDto;

namespace ServiceContracts.Interfaces.IWeightPlate
{
    public interface IWeightPlateGetterService
    {
        Task<IReadOnlyList<WeightPlateResponse>> GetAllWeightPlates();
        Task<WeightPlateResponse?> GetWeightPlateById(int id);
    }
}
