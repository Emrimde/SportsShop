using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IClothRepository
{
    Task<IEnumerable<Cloth>> GetAllClothes();
    Task<Cloth?> GetClothById(int id);
    IQueryable<Cloth> FilterClothes(string size, string gender, string type);
}

