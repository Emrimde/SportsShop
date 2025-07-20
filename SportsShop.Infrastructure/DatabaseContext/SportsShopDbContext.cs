using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.DatabaseContext
{
    public class SportsShopDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cloth> Clothes { get; set; }
        public virtual DbSet<Supplement> Supplements { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<WeightPlate> WeightPlates { get; set; }
        public virtual DbSet<TrainingRubber> TrainingRubbers { get; set; }
        public virtual DbSet<GymnasticRing> GymnasticRings { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);
        }

        public SportsShopDbContext(DbContextOptions<SportsShopDbContext> options) : base(options) { }

    }
}
