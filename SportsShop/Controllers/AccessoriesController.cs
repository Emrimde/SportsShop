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
        private readonly ILogger<AccessoriesController> _logger;

        public AccessoriesController(IAccessoryGetterService accessoriesService, IWeightPlateGetterService weightPlateGetterService, ITrainingRubberGetterService trainingRubberGetterService, IGymnasticRingGetterService gymnasticRingGetterService, ILogger<AccessoriesController> logger)
        {
            _accesoriesService = accessoriesService;
            _weightPlateGetterService = weightPlateGetterService;
            _trainingRubberGetterService = trainingRubberGetterService;
            _gymnasticRingGetterService = gymnasticRingGetterService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index action method started");

            AccessoriesViewModel accessories = new AccessoriesViewModel()
            {
                WeightPlates =  _weightPlateGetterService.GetAllWeightPlates(),
                GymnasticRings =  _gymnasticRingGetterService.GetAllGymnasticRings(),
                TrainingRubbers =  _trainingRubberGetterService.GetAllTrainingRubbers()
            };
            return View(accessories);
        }

        public async Task<IActionResult> ShowAccessory(int id, string type)
        {
            _logger.LogDebug("ShowAccessory action method started. Parameters id: {id}, type: {type}", id , type);
           
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

            _logger.LogWarning("Unknown accessory type");
            return NotFound();
        }

        public async Task<IActionResult> FilterAccessory(string type)
        {
            _logger.LogDebug("FilterAccessory action method started. Parameters type: {type}", type);

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
