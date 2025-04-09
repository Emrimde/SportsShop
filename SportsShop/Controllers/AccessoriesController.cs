using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using SportsShop.ViewModels;
using Entities.Models;
using Entities.DatabaseContext;
using ServiceContracts.Interfaces;

namespace SportsShop.Controllers
{
    public class AccessoriesController : Controller
    {
        
        private readonly IAccessoriesService _accesoriesService;

        public AccessoriesController(IAccessoriesService accessoriesService)
        {
            _accesoriesService = accessoriesService;
        }

        public async Task<IActionResult> Index()
        {
            AccessoriesViewModel accessories = new AccessoriesViewModel()
            {
                WeightPlates =await _accesoriesService.GetAllWeightPlates(),
                GymnasticRings = await _accesoriesService.GetAllGymnasticRings(),
                TrainingRubbers = await _accesoriesService.GetAllTrainingRubbers()
            };
            return View(accessories);
        }
    }
}
