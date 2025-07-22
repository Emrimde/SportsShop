using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsShop.Core.Domain.Models;
public class Drink
{
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string Type { get; set; } = default!;
    public string Flavor { get; set; } = default!;
    public string Volume { get; set; } = default!;
    public string VolumeUnit { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
