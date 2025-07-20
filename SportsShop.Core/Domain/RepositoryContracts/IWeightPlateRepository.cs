using Entities.Models;

namespace RepositoryContracts
{
    public interface IWeightPlateRepository
    {
        Task<IEnumerable<WeightPlate>> GetAllWeightPlates();
        Task<WeightPlate?> GetWeightPlateById(int id);
    }
}
