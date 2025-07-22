namespace SportsShop.Core.Domain.Models;
public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EditDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public bool IsActive { get; set; }
}
