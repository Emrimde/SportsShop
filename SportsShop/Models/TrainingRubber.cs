using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.Models
{
    public class TrainingRubber
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string Color { get; set; } = default!;
        public string Resistance { get; set; } = default!;
        public Product Product { get; set; } = default!;
        public string? ImagePath { get; set; } = default!;
        
    }
}
