using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces;
using ServiceContracts.Interfaces.ICloth;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothGetterService _clothGetterService;
        public ClothesController(SportsShopDbContext databaseContext,IClothGetterService clothesService)
        {
            _clothGetterService = clothesService;
        }
        public IActionResult Index()
        {
            List<ClothResponse> clothes = _clothGetterService.GetAllClothes();
            return View(clothes);
        }
        public async Task<IActionResult> ShowCloth(int id)
        {
            ClothResponse? cloth = await _clothGetterService.GetClothById(id);

            if (cloth == null)
            {
                return NotFound();
            }

            return View(cloth);
        }
        [HttpPost]
        public async Task<IActionResult> FilterCloth(string size, string gender, string type)
        {
            List<ClothResponse> clothes = await _clothGetterService.FilterClothes(size,gender,type);
            return View("Index",clothes);
        }
    }
}
