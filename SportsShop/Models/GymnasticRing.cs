namespace SportsShop.Models
{
    public class GymnasticRing
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int MaximumLoad { get; set; }
        public string Material { get; set; } = default!;
        public string TapeLength { get; set; } = default!;
    }
}
