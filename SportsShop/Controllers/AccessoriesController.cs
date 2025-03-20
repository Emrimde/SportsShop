using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly SportsShopDbContext DatabaseContext;

        public AccessoriesController(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            AccessoriesViewModel accessories = new AccessoriesViewModel()
            {
                WeightPlates = await DatabaseContext.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync(),
                GymnasticRings =await DatabaseContext.GymnasticRings.Include(item =>item.Product).Where(item => item.Product.IsActive).ToListAsync(),
                TrainingRubbers = await DatabaseContext.TrainingRubbers.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync()

            };
            return View(accessories);
        }
    }
}
