using Microsoft.AspNetCore.Mvc;
using SportsShop.Models;
using System.Diagnostics;

namespace SportsShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogDebug("Index action method");
        return View();
    }

    public IActionResult AboutUs()
    {
        _logger.LogDebug("About us action method");
        return View();
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
