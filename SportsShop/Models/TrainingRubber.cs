namespace SportsShop.Models
{
    public class TrainingRubber
    {
        
        public int ProductId { get; set; }
        public string Color { get; set; } = default!;
        public string Resistance { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
