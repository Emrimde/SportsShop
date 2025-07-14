using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.DatabaseContext;
using Entities.Models;
using Services;
using ServiceContracts.Interfaces.Account;
using Services.Accessory;
using Services.Account;
using ServiceContracts.Interfaces.IAccessory;
using ServiceContracts.Interfaces.IDrink;
using ServiceContracts.Interfaces.ISupplement;
using ServiceContracts.Interfaces.ICloth;
using ServiceContracts.Interfaces.IAddress;
using Services.IAddress;
using ServiceContracts.Interfaces.ICart;
using ServiceContracts.Interfaces.ISupplier;
using ServiceContracts.Interfaces.IOrder;
using ServiceContracts.Interfaces.IWeightPlate;
using ServiceContracts.Interfaces.IGymnasticRing;
using ServiceContracts.Interfaces.ITrainingRubber;
using RepositoryContracts;
using Repositories;
using Serilog;

namespace SportsShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .ReadFrom.Services(services)
                    .WriteTo.Console();
            });


            builder.Services.AddDbContext<SportsShopDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Entities")));

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
