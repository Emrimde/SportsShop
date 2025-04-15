using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.DatabaseContext;
using Entities.Models;
using Services;
using ServiceContracts.Interfaces;

namespace SportsShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SportsShopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Entities")));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDrinksService, DrinksService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccessoriesService, AccessoriesService>();
            builder.Services.AddScoped<ISupplementsService, SupplementsService>();
            builder.Services.AddScoped<IClothesService, ClothesService>();
            builder.Services.AddScoped<IAddressesService, AddressesService>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddIdentity<User, UserRole>(options =>
            {
               // options.User.RequireUniqueEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 4;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                
            })
                .AddEntityFrameworkStores<SportsShopDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
