using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.CountryDto;
using SportsShop.Core.ServiceContracts.Interfaces.ICountry;

namespace SportsShop.Core.Services.CountryServices;
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
