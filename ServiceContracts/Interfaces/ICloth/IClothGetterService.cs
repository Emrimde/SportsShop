using Entities.Models;

namespace ServiceContracts.Interfaces.ICloth
{
    public interface IClothGetterService
    {
        Task<List<Cloth>> GetAllClothes();
        Task<Cloth?> GetCloth(int id);
        Task<List<Cloth>> FilterCloth(string size, string gender, string type);
    }
}
