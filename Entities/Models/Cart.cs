namespace Entities.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public DateTime CreatedDate { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
