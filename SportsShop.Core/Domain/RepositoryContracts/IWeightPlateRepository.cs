using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IWeightPlateRepository
{
    Task<IEnumerable<WeightPlate>> GetAllWeightPlates();
    Task<WeightPlate?> GetWeightPlateById(int id);
}
