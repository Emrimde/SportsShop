namespace SportsShop.Core.Domain.Models;
public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime? DeletionDateTime { get; set; }
}
