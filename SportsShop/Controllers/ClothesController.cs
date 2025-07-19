using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;

namespace SportsShop.Controllers;
    public class ClothesController : Controller
    {
        private readonly IClothGetterService _clothGetterService;
        private readonly ILogger<ClothesController> _logger;
        public ClothesController(IClothGetterService clothesService, ILogger<ClothesController> logger)
        {
            _clothGetterService = clothesService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method of ClothController");

            IEnumerable<ClothResponse> clothes = await _clothGetterService.GetAllClothes();
            return View(clothes);
        }

        public async Task<IActionResult> ShowCloth(int id)
        {
            _logger.LogDebug("ShowCloth action method.Parameter id: {id}", id);
            
            ClothResponse? cloth = await _clothGetterService.GetClothById(id);

            if (cloth == null)
            {
                _logger.LogError("Cloth not found");
                return NotFound();
            }

            return View(cloth);
        }

        [HttpPost]
        public async Task<IActionResult> FilterCloth(string size, string gender, string type)
        {
            _logger.LogDebug("FilterCloth action method. Parameters: size: {size}, gender: {gender}, type: {type}", size, gender,type);
           
            List<ClothResponse> clothes = await _clothGetterService.FilterClothes(size,gender,type);
            return View("Index",clothes);
        }
    }
