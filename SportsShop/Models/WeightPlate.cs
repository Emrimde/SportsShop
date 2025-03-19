namespace SportsShop.Models
{
    public class WeightPlate
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public string Weight { get; set; } = default!;
        public string Type { get; set; } = default!;


    }
}
