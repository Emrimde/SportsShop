using Microsoft.AspNetCore.Mvc;
using SportsShop.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.DatabaseContext;

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
           
            var supplements = await DatabaseContext.Supplements
            .Include(s => s.Product) //Allows to access the Product navigation property
            .Where(s => s.Product.IsActive)
            .ToListAsync();
            return View(supplements);
        }


    }
}
