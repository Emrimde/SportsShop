namespace SportsShop.ViewModels
{
    public class ClothesViewModel
    {
        public int Id { get; set; }
        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string? Code { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
