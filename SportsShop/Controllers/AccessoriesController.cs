using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Interfaces.IAccessory;
using ServiceContracts.Interfaces.IGymnasticRing;
using ServiceContracts.Interfaces.ITrainingRubber;
using ServiceContracts.Interfaces.IWeightPlate;
using SportsShop.ViewModels;

namespace SportsShop.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly IAccessoryGetterService _accesoriesService;
        private readonly IWeightPlateGetterService _weightPlateGetterService;
        private readonly ITrainingRubberGetterService _trainingRubberGetterService;
        private readonly IGymnasticRingGetterService _gymnasticRingGetterService;

        public AccessoriesController(IAccessoryGetterService accessoriesService, IWeightPlateGetterService weightPlateGetterService, ITrainingRubberGetterService trainingRubberGetterService, IGymnasticRingGetterService gymnasticRingGetterService)
        {
            _accesoriesService = accessoriesService;
            _weightPlateGetterService = weightPlateGetterService;
            _trainingRubberGetterService = trainingRubberGetterService;
            _gymnasticRingGetterService = gymnasticRingGetterService;
        }

        public async Task<IActionResult> Index()
        {
            AccessoriesViewModel accessories = new AccessoriesViewModel()
            {
                WeightPlates = await _weightPlateGetterService.GetAllWeightPlates(),
                GymnasticRings = await _gymnasticRingGetterService.GetAllGymnasticRings(),
                TrainingRubbers = await _trainingRubberGetterService.GetAllTrainingRubbers()
            };
            return View(accessories);
        }

        public async Task<IActionResult> ShowAccessory(int id, string type)
        {
            if (type == "GymnasticRing")
            {
                GymnasticRingResponse? gymnasticRing = await _gymnasticRingGetterService.GetGymnasticRingById(id);
                return View("ShowGymnasticRing", gymnasticRing);
            }
            else if (type == "TrainingRubber")
            {
                TrainingRubberResponse? trainingRubber = await _trainingRubberGetterService.GetTrainingRubberById(id);
                return View("ShowTrainingRubber", trainingRubber);
            }
            else if (type == "WeightPlate")
            {
                WeightPlateResponse? weightPlate = await _weightPlateGetterService.GetWeightPlateById(id);
                return View("ShowWeightPlate", weightPlate);
            }
            return NotFound();
        }

        public async Task<IActionResult> FilterAccessory(string type)
        {
            List<dynamic> accessories = await _accesoriesService.FilterAccessory(type);
            if (accessories.Count == 0)
            {
                return RedirectToAction("Index");
            }
            AccessoriesViewModel accessoriesViewModels = new AccessoriesViewModel()
            {
                SpecificAccessories = accessories
            };
            
            return View("Index",accessoriesViewModels);
        }
    }
}
