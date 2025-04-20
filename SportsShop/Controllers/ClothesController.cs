using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;

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
        public async Task<IActionResult> ShowCloth(int id)
        {
            Cloth? cloth = await _clothesService.GetCloth(id);
            if (cloth == null)
            {
                return NotFound();
            }
            ClothesViewModel clothViewModel = new ClothesViewModel()
            {
               Id = cloth.ProductId,
                Name = cloth.Product.Name,
                Description = cloth.Product.Description,
                Price = cloth.Product.Price,
                Quantity = cloth.Product.Quantity,
                Size = cloth.Size,
                Color = cloth.Color,
                Material = cloth.Material,
                ImagePath = cloth.ImagePath,
                Producer = cloth.Product.Producer,
                Code = cloth.Product.Code,
                

            };
            return View(clothViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> FilterCloth(string size, string gender, string type)
        {
            List<Cloth> clothes = await _clothesService.FilterCloth(size,gender,type);
            return View("Index",clothes);
        }
    }
}
