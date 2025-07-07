using Microsoft.AspNetCore.Mvc;

namespace SportsShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string type, int id)
        {
            _logger.LogDebug("Index action method. Parameters: type: {type}, id: {id}", type, id);

            if (type == "Cloth")
                return RedirectToAction("ShowCloth", "Clothes", new { id = id });
            else if(type == "Drink")
                return RedirectToAction("ShowDrink", "Drinks", new {id=id});
            else if (type == "GymnasticRing" || type == "TrainingRubber" || type == "WeightPlate")
                return RedirectToAction("ShowAccessory", "Accessories", new {type= type, id = id});
            else if (type == "Supplement")
                return RedirectToAction("ShowSupplement", "ShowSupplement", new { id = id });
          
            return BadRequest();
        }
    }
}
