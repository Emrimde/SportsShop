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
        private readonly ILogger<AddressController> _logger;
        private readonly UserManager<User> _userManager;
        public AddressController(IAddressGetterService addressGetterService,IAddressDeleterService addressDeleterService, IAddressUpdaterService addressUpdaterService,IAddressAdderService addressAdderService, UserManager<User> userManager, ILogger<AddressController> logger)
        {
            _addressAdderService = addressAdderService;
            _addressUpdaterService = addressUpdaterService;
            _addressGetterService = addressGetterService;
            _addressDeleterService = addressDeleterService;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all user's addresses");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogError("User not found - Index action method");
                return Unauthorized();
            }

            List<AddressResponse> addresses = _addressGetterService.GetAllAddresses(user.Id);
            
            return View(addresses);
        }

        public async Task<IActionResult> AddAddress(AddressAddRequest addressAddRequest)
        {
            _logger.LogDebug("AddAddress action method adds address for specific user. Parameters: addressAddRequest: {dto}" , addressAddRequest.ToString());

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, user.Id);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        public async Task<IActionResult> DeleteAddress(int id)
        {
            _logger.LogDebug("DeleteAddress action method deletes address for specific user. Parameter: id: {id}", id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("User not found - DeleteAddress action method");
                return Unauthorized();
            }
            bool result = await _addressDeleterService.DeleteAddress(id);
            if (!result)
            {
                _logger.LogError("Invalid deletion in DeleteAddress action: id: {id}", id);
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAddress(int id)
        {
            _logger.LogDebug("EditAddress action returns edit view with address to edition. Parameter: id: {id}", id);

            AddressResponse? address = await _addressGetterService.GetAddressById(id);
            if (address == null)
            {
                _logger.LogError("Address not found in EditAddress action.");
                return NotFound();
            }
            
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(AddressUpdateRequest addressUpdateRequest)
        {
            _logger.LogDebug("[HttpPost]EditAddress action method edits address for specific user. Parameter: addressUpdateRequest: {addressUpdateRequest}", addressUpdateRequest.ToString());

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogError("Address edition failed. User is not authorized");
                return Unauthorized();
            }

            if (addressUpdateRequest == null)
            {
                return BadRequest();
            }

            await _addressUpdaterService.UpdateAddress(addressUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}