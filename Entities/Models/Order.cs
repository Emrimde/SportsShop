using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Guid UserId { get; set; }

        public User User { get; set; } = default!;
        public int AddressId { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Coupon { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsActive { get; set; }
    }
}
