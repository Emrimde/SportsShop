namespace SportsShop.Core.Domain.Models;
public class Address
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public User User { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = default!;
    public DateTime? EditDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public bool IsActive { get; set; }
}

