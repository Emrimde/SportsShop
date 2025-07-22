using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.ICountry;

namespace SportsShop.Core.Services.CountryServices;
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
