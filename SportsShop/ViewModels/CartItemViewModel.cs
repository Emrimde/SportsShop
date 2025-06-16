namespace SportsShop.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
    }
}
