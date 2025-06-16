using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces.ISupplement;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class SupplementsController : Controller
    {
        private readonly ISupplementGetterService _supplementGetterService;

        public SupplementsController(ISupplementGetterService supplementGetterService)
        {
            _supplementGetterService = supplementGetterService;
        }

        public async Task<IActionResult> Index()
        {

            List<Supplement> supplements = await _supplementGetterService.GetAllSupplements();
            List<SupplementsViewModel> supplementsViewModels = new List<SupplementsViewModel>();
            foreach (var supplement in supplements)
            {
                SupplementsViewModel supplementViewModel = new SupplementsViewModel()
                {
                    Id = supplement.ProductId,
                    Name = supplement.Product.Name,
                    Description = supplement.Product.Description,
                    Price = supplement.Product.Price,
                    Flavor = supplement.Flavor,
                    Weight = supplement.Weight,
                    Type = supplement.Type,
                    ImagePath = supplement.ImagePath
                };
                supplementsViewModels.Add(supplementViewModel);
            }
            return View(supplementsViewModels);
        }

        public async Task<IActionResult> ShowSupplement(int id)
        {
            Supplement? supplement = await _supplementGetterService.GetSupplement(id);
            if (supplement == null)
            {
                return NotFound();
            }
            SupplementsViewModel supplementViewModel = new SupplementsViewModel()
            {
                Id = supplement.ProductId,
                Description = supplement.Product.Description,
                Name = supplement.Product.Name,
                Price = supplement.Product.Price,
                ImagePath = supplement.ImagePath,
                Weight = supplement.Weight
            };
            return View(supplementViewModel);
        }
        public async Task<IActionResult> FilterSupplement(string type, string flavor)
        {
            List<Supplement> supplements = await _supplementGetterService.FilterSupplement(type, flavor);
            List<SupplementsViewModel> supplementsViewModels = new List<SupplementsViewModel>();
            foreach(var supplement in supplements)
            {
                SupplementsViewModel supplementViewModel = new SupplementsViewModel()
                {
                    Id = supplement.ProductId,
                    Name = supplement.Product.Name,
                    Description = supplement.Product.Description,
                    Price = supplement.Product.Price,
                    Flavor = supplement.Flavor,
                    Weight = supplement.Weight,
                    Type = supplement.Type,
                    ImagePath = supplement.ImagePath
                };
                supplementsViewModels.Add(supplementViewModel);
            }
            return View("Index", supplementsViewModels);
        }
    }
}
