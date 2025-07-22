using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.Core.Domain.Models;
public class WeightPlate
{
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public string Weight { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? ImagePath { get; set; } = default!;
}
