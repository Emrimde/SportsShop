using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;

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
        public async Task<IActionResult> ShowAccessory(int id, string type)
        {
            
            
            if (type == "GymnasticRing")
            {
                GymnasticRing? gymnasticRing = await _accesoriesService.GetGymnasticRing(id);
                return View("ShowGymnasticRing",gymnasticRing);
            }
            else if (type == "TrainingRubber")
            {
                TrainingRubber? trainingRubber = await _accesoriesService.GetTrainingRubber(id);
                return View("ShowTrainingRubber",trainingRubber);
            }
            else if (type == "WeightPlate")
            {
                WeightPlate? weightPlate = await _accesoriesService.GetWeightPlate(id);
                return View("ShowWeightPlate",weightPlate);
            }

            return NotFound();

        }
    }
}
