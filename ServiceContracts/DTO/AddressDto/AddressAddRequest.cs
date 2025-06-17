using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace ServiceContracts.DTO.AddressDto
{
    public class AddressAddRequest
    {
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; } = default!;

        public Address ToAddress(Guid userId)
        {
            return new Address()
            {
                UserId = userId,
                Country = Country,
                City = City,
                Street = Street,
                ZipCode = ZipCode,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
        }
    }
}
