using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.ServiceContracts.DTO.CountryDto;
public class CountryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
public static class CountryResponseExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse 
        { 
            Id = country.Id, 
            Name = country.Name 
        };
    }
}
