using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsShop.Models
{
    public class SportsShopDbContext : IdentityDbContext<User,UserRole,Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WeightPlate> WeightPlates { get; set; }
        public DbSet<TrainingRubber> TrainingRubbers { get; set; }
        public DbSet<GymnasticRing> GymnasticRings { get; set; }
        public DbSet<Drink> Drinks { get; set; }

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
