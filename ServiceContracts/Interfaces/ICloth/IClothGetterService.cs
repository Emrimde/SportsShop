using Entities.Models;
using ServiceContracts.DTO.ClothDto;

namespace ServiceContracts.Interfaces.ICloth
{
    public interface IClothGetterService
    {
        Task<List<ClothResponse>> GetAllClothess();
        Task<ClothResponse?> GetClothById(int id);
        Task<List<ClothResponse>> FilterClothes(string size, string gender, string type);
    }
}
