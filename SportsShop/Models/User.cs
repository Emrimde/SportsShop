using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsShop.Models
{
    public class User : IdentityUser<Guid>
    {
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        [ValidateNever]
        public Cart Cart { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = default!;
        public DateTime? LastLoginDate { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }




    }
}
