using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsShop.Core.Domain.Models;
public class GymnasticRing
{
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int MaximumLoad { get; set; }
    public string Material { get; set; } = default!;
    public string TapeLength { get; set; } = default!;
    public string? ImagePath { get; set; } = default!;
}
