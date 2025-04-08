using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Cloth
    {
        [Key] 
        [ForeignKey("Product")] 
        public int ProductId { get; set; }

        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;

        public string ImagePath { get; set; } = default!;   

        public Product Product { get; set; } = default!;
    }
}
