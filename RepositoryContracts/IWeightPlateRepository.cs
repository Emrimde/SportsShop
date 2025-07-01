using Entities.Models;

namespace RepositoryContracts
{
    public interface IWeightPlateRepository
    {
        IQueryable<WeightPlate> GetAllWeightPlates();
        Task<WeightPlate?> GetWeightPlateById(int id);
    }
}
