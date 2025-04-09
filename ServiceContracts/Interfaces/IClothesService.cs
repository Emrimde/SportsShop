using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IClothesService
    {
        Task<List<Cloth>> GetAllClothes();
    }
}
