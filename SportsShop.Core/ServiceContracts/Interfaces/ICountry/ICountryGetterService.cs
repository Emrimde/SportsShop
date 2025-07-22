using SportsShop.Core.ServiceContracts.DTO.CountryDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ICountry;
public interface ICountryGetterService
{
    Task<IEnumerable<CountryResponse>> GetAllCountries();
}
