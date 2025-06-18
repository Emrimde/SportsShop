using Entities.Models;

namespace ServiceContracts.DTO.AddressDto
{
    public class AddressUpdateRequest
    {
        public int Id { get; set; }
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string ZipCode { get; set; } = default!;

        public Address ToAddress()
        {
            return new Address
            {
                Id = Id,
                Country = Country,
                City = City,
                Street = Street,
                ZipCode = ZipCode
            };
        }
    }
}
