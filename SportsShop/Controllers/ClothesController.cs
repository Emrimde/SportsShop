using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;

namespace SportsShop.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesService _clothesService;

        public ClothesController(SportsShopDbContext databaseContext,IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        public async Task<IActionResult> Index()
        {
            List<Cloth> clothes = await _clothesService.GetAllClothes();
            return View(clothes);
        }
    }
}
