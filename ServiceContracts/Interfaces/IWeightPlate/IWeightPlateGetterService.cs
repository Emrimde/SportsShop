using ServiceContracts.DTO.WeightPlateDto;

namespace ServiceContracts.Interfaces.IWeightPlate
{
    public interface IWeightPlateGetterService
    {
        Task<List<WeightPlateResponse>> GetAllWeightPlates();
        Task<WeightPlateResponse?> GetWeightPlateById(int id);
    }
}
