using Entities.Models;
using RepositoryContracts;
using ServiceContracts.DTO.CountryDto;
using ServiceContracts.Interfaces.ICountry;

namespace Services
{
    public class CountryGetterService : ICountryGetterService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryGetterService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryResponse>> GetAllCountries()
        {
            IEnumerable<Country> countries = await _countryRepository.GetAllCountries();

            return countries.Select(item => item.ToCountryResponse()).ToList();
        }
    }
}
