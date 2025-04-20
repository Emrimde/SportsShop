using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;
using System.Threading.Tasks;

namespace SportsShop.Controllers
{
    public class SupplementsController : Controller
    {
        private readonly ISupplementsService _supplementsService;

        public SupplementsController(ISupplementsService supplementsService)
        {
            _supplementsService = supplementsService;
        }

        public async Task<IActionResult> Index()
        {

            List<Supplement> supplements = await _supplementsService.GetAllSupplements();
            return View(supplements);
        }

        public async Task<IActionResult> ShowSupplement(int id)
        {
            Supplement? supplement = await _supplementsService.GetSupplement(id);
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
            List<Supplement> supplements = await _supplementsService.FilterSupplement(type, flavor);
            return View("Index", supplements);
        }
    }
}
