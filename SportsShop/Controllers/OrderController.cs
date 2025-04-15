using Microsoft.AspNetCore.Mvc;

namespace SportsShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
