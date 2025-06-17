using Entities.Models;

namespace ServiceContracts.DTO.AddressDto
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string Country { get; set; } = default!;
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
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
        }
    }
}
