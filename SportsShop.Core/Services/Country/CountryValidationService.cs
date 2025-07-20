using RepositoryContracts;
using ServiceContracts.Interfaces.ICountry;

namespace Services
{
    public class CountryValidationService : ICountryValidationService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryValidationService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<bool> IsCountryValid(int countryId)
        {
            return await _countryRepository.CountryExists(countryId);
        }
    }
}
