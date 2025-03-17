using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportsShop.Models
{
    public class Supplement
    {
        [Key] // ✅ Oznacza 'ProductId' jako klucz główny
        [ForeignKey("Product")] // ✅ Relacja 1:1 do Product
        public int ProductId { get; set; }

        public string Flavor { get; set; } = default!;

        public string Weight { get; set; } = default!;

        public string Type { get; set; } = default!;

        public string ImagePath { get; set; } = default!;
        public Product Product { get; set; } = default!;

    }
}
