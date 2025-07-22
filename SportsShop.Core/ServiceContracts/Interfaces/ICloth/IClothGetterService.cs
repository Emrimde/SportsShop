using SportsShop.Core.ServiceContracts.DTO.ClothDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ICloth;
public interface IClothGetterService
{
    Task<IEnumerable<ClothResponse>> GetAllClothes();
    Task<ClothResponse?> GetClothById(int id);
    Task<List<ClothResponse>> FilterClothes(string size, string gender, string type);
}
