using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.Account;
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
        private readonly IAccountService _accountService;
        public AddressController(IAddressGetterService addressGetterService,IAddressDeleterService addressDeleterService, IAddressUpdaterService addressUpdaterService,IAddressAdderService addressAdderService, ILogger<AddressController> logger, IAccountService accountService)
        {
            _addressAdderService = addressAdderService;
            _addressUpdaterService = addressUpdaterService;
            _addressGetterService = addressGetterService;
            _addressDeleterService = addressDeleterService;
            _accountService = accountService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all user's addresses");
  
            string? userId = _accountService.GetUserId(User);
            IEnumerable<AddressResponse> addresses = await _addressGetterService.GetAllAddresses(userId!);

            return View(addresses);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressAddRequest addressAddRequest)
        {
            _logger.LogDebug("AddAddress action method adds address for specific user. Parameters: addressAddRequest: {dto}" , addressAddRequest.ToString());

            if (!ModelState.IsValid)
            {
                return View(addressAddRequest);
            }

            string? user2 = _accountService.GetUserId(User);
            AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, user2!);
            if (result != null)
                {
                    return RedirectToAction("Index");
                }
            
            return BadRequest();
        }

        [Authorize]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            _logger.LogDebug("DeleteAddress action method deletes address for specific user. Parameter: id: {id}", id);

            bool result = await _addressDeleterService.DeleteAddress(id);
            if (!result)
            {
                _logger.LogError("Invalid deletion in DeleteAddress action: id: {id}", id);
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> EditAddress(AddressUpdateRequest addressUpdateRequest)
        {
            _logger.LogDebug("[HttpPost]EditAddress action method edits address for specific user. Parameter: addressUpdateRequest: {addressUpdateRequest}", addressUpdateRequest.ToString());

            if (addressUpdateRequest == null)
            {
                return BadRequest();
            }

            await _addressUpdaterService.UpdateAddress(addressUpdateRequest);
            return RedirectToAction("Index");
        }
    }
}