using Entities.Models;

namespace RepositoryContracts
{
    public interface IClothRepository
    {
        IQueryable<Cloth> GetAllClothes();
        Task<Cloth?> GetClothById(int id);
        IQueryable<Cloth> FilterClothes(string size, string gender, string type);
    }
}
