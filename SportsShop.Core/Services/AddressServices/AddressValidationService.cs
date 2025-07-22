using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.DTO.AddressDto;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.ServiceContracts.Interfaces.ICountry;

namespace SportsShop.Core.Services.AddressServices
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
