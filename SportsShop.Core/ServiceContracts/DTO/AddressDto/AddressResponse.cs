using Entities.Models;

namespace ServiceContracts.DTO.AddressDto
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = default!;
        public int CountryId { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
    }
    public static class AddressResponseExtensions
    {
        public static AddressResponse ToAddressResponse(this Address address)
        {
            return new AddressResponse
            {
                Id = address.Id,
                CountryName = address.Country.Name,
                CountryId = address.Country.Id,
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
        }

        public static AddressUpdateRequest ToUpdateRequest(this AddressResponse addressResponse)
        {
            return new AddressUpdateRequest
            {
                Id = addressResponse.Id,
                CountryId = addressResponse.CountryId,
                City = addressResponse.City,
                Street = addressResponse.Street,
                ZipCode = addressResponse.ZipCode
            };
        }
    }
}
