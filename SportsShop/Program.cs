using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using SportsShop.Core.Domain.Models;
using SportsShop.Core.Domain.RepositoryContracts;
using SportsShop.Core.ServiceContracts.Interfaces.IAccessory;
using SportsShop.Core.ServiceContracts.Interfaces.IAccount;
using SportsShop.Core.ServiceContracts.Interfaces.IAddress;
using SportsShop.Core.ServiceContracts.Interfaces.ICart;
using SportsShop.Core.ServiceContracts.Interfaces.ICloth;
using SportsShop.Core.ServiceContracts.Interfaces.ICountry;
using SportsShop.Core.ServiceContracts.Interfaces.IDrink;
using SportsShop.Core.ServiceContracts.Interfaces.IGymnasticRing;
using SportsShop.Core.ServiceContracts.Interfaces.IOrder;
using SportsShop.Core.ServiceContracts.Interfaces.IProduct;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplement;
using SportsShop.Core.ServiceContracts.Interfaces.ISupplier;
using SportsShop.Core.ServiceContracts.Interfaces.ITrainingRubber;
using SportsShop.Core.ServiceContracts.Interfaces.IWeightPlate;
using SportsShop.Core.Services.AddressServices;
using SportsShop.Core.Services.CartServices;
using SportsShop.Core.Services.ClothServices;
using SportsShop.Core.Services.CountryServices;
using SportsShop.Core.Services.DrinkServices;
using SportsShop.Core.Services.GymnasticRingServices;
using SportsShop.Core.Services.OrderServices;
using SportsShop.Core.Services.ProductServices;
using SportsShop.Core.Services.SupplementServices;
using SportsShop.Core.Services.SupplierServices;
using SportsShop.Core.Services.TrainingRubberServices;
using SportsShop.Core.Services.WeightPlateServices;
using SportsShop.Core.Services.AccessoryServices;
using SportsShop.Core.Services.AccountServices;
using SportsShop.Infrastructure.DatabaseContext;
using SportsShop.Infrastructure.Repositories;
using SportsShop.UI.Builders.CheckoutBuilderService;
using SportsShop.Core.Services.AddressService;

namespace SportsShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<SportsShopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDrinkGetterService, DrinkGetterService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccessoryGetterService, AccessoryGetterService>();
            builder.Services.AddScoped<ISupplementGetterService, SupplementGetterService>();
            builder.Services.AddScoped<IClothGetterService, ClothGetterService>();
            builder.Services.AddScoped<IAddressGetterService, AddressGetterService>();
            builder.Services.AddScoped<IAddressUpdaterService, AddressUpdaterService>();
            builder.Services.AddScoped<IAddressDeleterService, AddressDeleterService>();
            builder.Services.AddScoped<IAddressAdderService, AddressAdderService>();
            builder.Services.AddScoped<ICartGetterService, CartGetterService>();
            builder.Services.AddScoped<ICartAdderService, CartAdderService>();
            builder.Services.AddScoped<ICartUpdaterService, CartUpdaterService>();
            builder.Services.AddScoped<ICartDeleterService, CartDeleterService>();
            builder.Services.AddScoped<ISupplierGetterService, SupplierGetterService>();
            builder.Services.AddScoped<ICountryValidationService, CountryValidationService>();
            builder.Services.AddScoped<IAddressValidationService, AddressValidationService>();
            builder.Services.AddScoped<IProductValidationService, ProductValidationService>();
            builder.Services.AddScoped<ICountryGetterService, CountryGetterService>();
            builder.Services.AddScoped<ICheckoutBuilderService, CheckoutBuilderService>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderAdderService, OrderAdderService>();
            builder.Services.AddScoped<IOrderGetterService, OrderGetterService>();
            builder.Services.AddScoped<IWeightPlateGetterService, WeightPlateGetterService>();
            builder.Services.AddScoped<IGymnasticRingGetterService, GymnasticRingGetterService>();
            builder.Services.AddScoped<ITrainingRubberGetterService, TrainingRubberGetterService>();
            builder.Services.AddScoped<IClothRepository, ClothRepository>();
            builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
            builder.Services.AddScoped<IGymnasticRingRepository, GymnasticRingRepository>();
            builder.Services.AddScoped<ISupplementRepository,SupplementRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IWeightPlateRepository, WeightPlateRepository>();
            builder.Services.AddScoped<ITrainingRubberRepository, TrainingRubberRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IAccessoryRepository, AccessoryRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();

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

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
            });

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
