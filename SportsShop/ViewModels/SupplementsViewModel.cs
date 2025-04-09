namespace SportsShop.ViewModels
{
    public class SupplementsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Flavor { get; set; } = default!;
        public string Weight { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
    }
}
