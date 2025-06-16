using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            List<Cloth> clothes = await _clothGetterService.GetAllClothes();
            List<ClothesViewModel> clothesViewModel = new List<ClothesViewModel>();
            foreach (var cloth in clothes)
            {
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
                    Code = cloth.Product.Code
                };
                clothesViewModel.Add(clothViewModel);
            }

            return View(clothesViewModel);
        }
        public async Task<IActionResult> ShowCloth(int id)
        {
            Cloth? cloth = await _clothGetterService.GetCloth(id);

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
            List<Cloth> clothes = await _clothGetterService.FilterCloth(size,gender,type);
            List<ClothesViewModel> clothesViewModel = new List<ClothesViewModel>();
            foreach (var cloth in clothes)
            {
                ClothesViewModel clothViewModel = new ClothesViewModel()
                {
                    Id = cloth.ProductId,
                    //Type = cloth.Type!,
                    //Gender = cloth.Gender!,
                    Name = cloth.Product.Name,
                    Description = cloth.Product.Description,
                    Price = cloth.Product.Price,
                    Quantity = cloth.Product.Quantity,
                    Size = cloth.Size,
                    Color = cloth.Color,
                    Material = cloth.Material,
                    ImagePath = cloth.ImagePath,
                    Producer = cloth.Product.Producer,
                    Code = cloth.Product.Code
                };
                clothesViewModel.Add(clothViewModel);
            }

            return View("Index",clothesViewModel);
        }
    }
}
