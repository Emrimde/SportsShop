using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShop.Models;
using Entities.DatabaseContext; 

namespace SportsShop.Views.Shared.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        private SportsShopDbContext DatabaseContext;

        public UserInfoViewComponent(SportsShopDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? userId = HttpContext.Session.GetInt32("User");
            return View("Default");


        }
    }
}
