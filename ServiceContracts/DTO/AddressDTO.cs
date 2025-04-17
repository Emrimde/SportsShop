using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class AddressDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; } = default!;
    }
}
