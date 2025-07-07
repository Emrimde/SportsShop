using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.SupplementDto;
using ServiceContracts.Interfaces.ISupplement;

namespace SportsShop.Controllers
{
    public class SupplementsController : Controller
    {
        private readonly ISupplementGetterService _supplementGetterService;
        private readonly ILogger<SupplementsController> _logger;

        public SupplementsController(ISupplementGetterService supplementGetterService, ILogger<SupplementsController> logger)
        {
            _supplementGetterService = supplementGetterService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index action method. Displays all supplements");

            List<SupplementResponse> supplements =  _supplementGetterService.GetAllSupplements();
            return View(supplements);
        }

        public async Task<IActionResult> ShowSupplement(int id)
        {
            _logger.LogDebug("ShowSupplement action method. Parameter: id:{id}", id);

            SupplementResponse ? supplement = await _supplementGetterService.GetSupplementById(id);

            if (supplement == null)
            {
                _logger.LogWarning("Supplement not found");
                return NotFound();
            }
            
            return View(supplement);
        }
        public async Task<IActionResult> FilterSupplement(string type, string flavor)
        {
            _logger.LogDebug("FilterSupplement action method. Parameters: type:{type}, flavor: {flavor}", type, flavor);
            
            List<SupplementResponse> supplements = await _supplementGetterService.FilterSupplements(type, flavor);
            return View("Index", supplements);
        }
    }
}
