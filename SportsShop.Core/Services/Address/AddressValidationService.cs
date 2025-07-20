using RepositoryContracts;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICountry;

namespace Services
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryValidationService _countryValidationService;
        public AddressValidationService(IAddressRepository addressRepository, ICountryValidationService countryValidationService)
        {
            _addressRepository = addressRepository;
            _countryValidationService = countryValidationService;
        }

        public async Task<bool> IsAddressValid(AddressAddRequest addressAddRequest)
        {
            if (char.IsUpper(addressAddRequest.City[0]) && char.IsUpper(addressAddRequest.Street[0]) && await _countryValidationService.IsCountryValid(addressAddRequest.CountryId))
            {
                return true;
            }
            return false;
        }
    }
}
