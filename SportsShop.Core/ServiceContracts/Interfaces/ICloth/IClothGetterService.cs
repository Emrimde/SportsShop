using ServiceContracts.DTO.ClothDto;

namespace ServiceContracts.Interfaces.ICloth
{
    public interface IClothGetterService
    {
        Task<IEnumerable<ClothResponse>> GetAllClothes();
        Task<ClothResponse?> GetClothById(int id);
        Task<List<ClothResponse>> FilterClothes(string size, string gender, string type);
    }
}
