﻿using SportsShop.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.Core.ServiceContracts.DTO.AddressDto;
public class AddressAddRequest
{
    [Required(ErrorMessage = "Country is required")]
    public int CountryId { get; set; } = default!;

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
            CountryId = CountryId,
            City = City!,
            Street = Street!,
            ZipCode = ZipCode!,
        };
    }

    public override string ToString()
    {
        return $"{{{nameof(CountryId)}={CountryId}, {nameof(City)}={City}, {nameof(Street)}={Street}, {nameof(ZipCode)}={ZipCode}}}";
    }
}
