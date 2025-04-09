using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;

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
    }
}
