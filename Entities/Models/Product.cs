namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string? Code { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }
    }
}
