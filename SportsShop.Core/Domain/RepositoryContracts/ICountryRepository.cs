using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountries();
    Task<bool> CountryExists(int countryId);
}
