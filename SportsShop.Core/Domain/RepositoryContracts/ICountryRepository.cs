using Entities.Models;

namespace RepositoryContracts
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<bool> CountryExists(int countryId);
    }
}
