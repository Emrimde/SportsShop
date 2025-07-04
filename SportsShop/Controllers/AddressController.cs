using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

namespace SportsShop.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressAdderService _addressAdderService;
        private readonly IAddressGetterService _addressGetterService;
        private readonly IAddressUpdaterService _addressUpdaterService;
        private readonly IAddressDeleterService _addressDeleterService;
        private readonly UserManager<User> _userManager;
        public AddressController(IAddressGetterService addressGetterService,IAddressDeleterService addressDeleterService, IAddressUpdaterService addressUpdaterService,IAddressAdderService addressAdderService, UserManager<User> userManager)
        {
            _addressAdderService = addressAdderService;
            _addressUpdaterService = addressUpdaterService;
            _addressGetterService = addressGetterService;
            _addressDeleterService = addressDeleterService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            List<AddressResponse> addresses = _addressGetterService.GetAllAddresses(user.Id);
            return View(addresses);
        }

        public async Task<IActionResult> AddAddress(AddressAddRequest model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            AddressResponse? result = await _addressAdderService.AddAddress(model, user.Id);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        public async Task<IActionResult> DeleteAddress(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            bool result = await _addressDeleterService.DeleteAddress(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAddress(int id)
        {
            AddressResponse? address = await _addressGetterService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(AddressUpdateRequest model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            if (model == null)
            {
                return BadRequest();
            }

            await _addressUpdaterService.UpdateAddress(model);
            return RedirectToAction("Index");
        }
    }
}