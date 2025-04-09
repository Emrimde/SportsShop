using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces;
using SportsShop.ViewModels;
using System.Security.Claims;

namespace SportsShop.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressesService _addressesService;
        private readonly UserManager<User> _userManager;
        public AddressController(IAddressesService addressesService, UserManager<User> userManager)
        {
            _addressesService = addressesService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddAddress(AddressDTO model)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _addressesService.AddAddress(model, user);

            if (result)
            {
                return RedirectToAction("ShowAddresses");
            }

            return Unauthorized();
        }

        public async Task<IActionResult> ShowAddresses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            List<Address> addresses = await _addressesService.ShowAddresses(user.Id);
            List<AddressViewModel> addressesViewModel = addresses.Select(item => new AddressViewModel()
            {
                Id = item.Id,
                Country = item.Country,
                City = item.City,
                Street = item.Street,
                ZipCode = item.ZipCode
            }).ToList();

            return View(addressesViewModel);
        }


        public async Task<IActionResult> DeleteAddress(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            bool result = await _addressesService.DeleteAddress(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("ShowAddresses");
        }


        public async Task<IActionResult> EditAddress(int id)
        {
            Address? address = await _addressesService.GetAddress(id);
            if (address == null)
            {
                return NotFound();
            }
            AddressViewModel addressViewModel = new AddressViewModel
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
            return View("EditAddress", addressViewModel);
        }

        public async Task<IActionResult> EditAddresss(AddressViewModel addressViewModel)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            Address? address = await _addressesService.GetAddress(addressViewModel.Id);

            if (address == null)
            {
                return NotFound();
            }
            await _addressesService.EditAddress(new AddressDTO
            {
                Id = addressViewModel.Id,
                Country = addressViewModel.Country,
                City = addressViewModel.City,
                Street = addressViewModel.Street,
                ZipCode = addressViewModel.ZipCode
            });

            return RedirectToAction("ShowAddresses");
        }
    }
}
