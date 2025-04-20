using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IClothesService
    {
        Task<List<Cloth>> GetAllClothes();
        Task<Cloth?> GetCloth(int id);
        Task<List<Cloth>> FilterCloth(string size, string gender, string type);
    }
}
