using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsShop.Models
{
    public class Cloth
    {
        [Key] // ✅ Oznacza 'ProductId' jako klucz główny
        [ForeignKey("Product")] // ✅ Relacja 1:1 do Product
        public int ProductId { get; set; }

        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;

        public string ImagePath { get; set; } = default!;   

        public Product Product { get; set; } = default!;
    }
}
