using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Supplement
    {
        [Key] 
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Flavor { get; set; } = default!;

        public string Weight { get; set; } = default!;

        public string Type { get; set; } = default!;

        public string ImagePath { get; set; } = default!;
        public Product Product { get; set; } = default!;

    }
}
