using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AddressDto
{
    public class AddressUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; } = default!;
        [Required]
        public string City { get; set; } = default!;
        [Required]
        public string Street { get; set; } = default!;
        [Required]
        public string ZipCode { get; set; } = default!;

        public Address ToAddress()
        {
            return new Address
            {
                Id = Id,
                CountryId = CountryId,
                City = City,
                Street = Street,
                ZipCode = ZipCode
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(CountryId)}={CountryId}, {nameof(City)}={City}, {nameof(Street)}={Street}, {nameof(ZipCode)}={ZipCode}}}";
        }
    }
    
}