namespace SportsShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }  
        public Cart Cart { get; set; } = default!;

        public int ProductId { get; set; } 
        public Product Product { get; set; } = default!;

        public int Quantity { get; set; } = 1; 
        public decimal Price { get; set; }  
    }
}
