using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ISupplement;

namespace SportsShop.Controllers
{
    public class SupplementsController : Controller
    {
        private readonly ISupplementGetterService _supplementGetterService;

        public SupplementsController(ISupplementGetterService supplementGetterService)
        {
            _supplementGetterService = supplementGetterService;
        }

        public IActionResult Index()
        {
            List<SupplementResponse> supplements =  _supplementGetterService.GetAllSupplements();
            return View(supplements);
        }

        public async Task<IActionResult> ShowSupplement(int id)
        {
            SupplementResponse? supplement = await _supplementGetterService.GetSupplementById(id);

            if (supplement == null)
            {
                return NotFound();
            }
            
            return View(supplement);
        }
        public async Task<IActionResult> FilterSupplement(string type, string flavor)
        {
            List<SupplementResponse> supplements = await _supplementGetterService.FilterSupplements(type, flavor);
            return View("Index", supplements);
        }
    }
}
