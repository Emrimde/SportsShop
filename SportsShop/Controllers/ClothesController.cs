using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;

namespace SportsShop.Controllers
{
    public class ClothesController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;

        public ClothesController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            var clothes = await DatabaseContext.Clothes.Include(item => item.Product)
                .Where(item => item.Product.IsActive).ToListAsync();
            return View(clothes);
        }
    }
}
