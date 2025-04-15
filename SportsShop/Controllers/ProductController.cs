using Microsoft.AspNetCore.Mvc;

namespace SportsShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(string type, int id)
        {
            if(type == "Cloth")
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
