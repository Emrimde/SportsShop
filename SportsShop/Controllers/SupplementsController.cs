using Microsoft.AspNetCore.Mvc;
using SportsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsShop.Controllers
{
    public class SupplementsController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;

        public SupplementsController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            //var supplements = await DatabaseContext.Supplements.Where(item => item.Product.IsActive).ToListAsync();
            var supplements = await DatabaseContext.Supplements
    .Include(s => s.Product) // ✅ Dołączenie relacji Product
    .Where(s => s.Product != null && s.Product.IsActive)
    .ToListAsync();
            return View(supplements);
        }


    }
}
