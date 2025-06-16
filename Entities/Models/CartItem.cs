namespace Entities.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; } = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public string? Type { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = default!;
        public DateTime? EditDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; } = null!;
    }
}
