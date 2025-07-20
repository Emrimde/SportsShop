using ServiceContracts.DTO.CountryDto;

namespace ServiceContracts.Interfaces.ICountry
{
    public interface ICountryGetterService
    {
        Task<IEnumerable<CountryResponse>> GetAllCountries();
    }
}
