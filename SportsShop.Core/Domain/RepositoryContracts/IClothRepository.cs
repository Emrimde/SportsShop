using Entities.Models;

namespace RepositoryContracts
{
    public interface IClothRepository
    {
        Task<IEnumerable<Cloth>> GetAllClothes();
        Task<Cloth?> GetClothById(int id);
        IQueryable<Cloth> FilterClothes(string size, string gender, string type);
    }
}
