using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Enums;
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

        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method started");

            AccessoriesViewModel accessories = new AccessoriesViewModel()
            {
                WeightPlates =  await _weightPlateGetterService.GetAllWeightPlates(),
                GymnasticRings = await  _gymnasticRingGetterService.GetAllGymnasticRings(),
                TrainingRubbers = await _trainingRubberGetterService.GetAllTrainingRubbers()
            };
            return View(accessories);
        }

        public async Task<IActionResult> ShowAccessory(int id, AccessoryTypeEnum? type)
        {
            _logger.LogDebug("ShowAccessory action method started. Parameters id: {id}, type: {type}", id , type);

            if (!type.HasValue || !Enum.IsDefined(typeof(AccessoryTypeEnum), type.Value))
            {
                _logger.LogWarning("Invalid Accessory type");
                return NotFound();
            }

            if (id <= 0)
            {
                _logger.LogWarning("Invalid accessory id: {id}", id);
                return NotFound();
            }

            if (type == AccessoryTypeEnum.GymnasticRing)
            {
                GymnasticRingResponse? gymnasticRing = await _gymnasticRingGetterService.GetGymnasticRingById(id);

                if (gymnasticRing == null) {
                    return NotFound();
                }

                return View("ShowGymnasticRing", gymnasticRing);
            }
            else if (type == AccessoryTypeEnum.TrainingRubber)
            {
                TrainingRubberResponse? trainingRubber = await _trainingRubberGetterService.GetTrainingRubberById(id);

                if (trainingRubber == null)
                {
                    return NotFound();
                }

                return View("ShowTrainingRubber", trainingRubber);
            }
            else if (type == AccessoryTypeEnum.WeightPlate)
            {
                WeightPlateResponse? weightPlate = await _weightPlateGetterService.GetWeightPlateById(id);

                if (weightPlate == null)
                {
                    return NotFound();
                }

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
            ViewBag.Type = type;
            return View("Index",accessoriesViewModels);
        }
    }
}
