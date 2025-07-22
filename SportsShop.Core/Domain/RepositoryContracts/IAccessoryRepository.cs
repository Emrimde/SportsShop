namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IAccessoryRepository
{
    Task<List<dynamic>> FilterAccessory(string type);
    Task<dynamic> GetObject(int id);
}
