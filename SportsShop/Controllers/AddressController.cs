using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.DTO.CountryDto;
using ServiceContracts.Interfaces.Account;
using ServiceContracts.Interfaces.IAddress;
using ServiceContracts.Interfaces.ICountry;

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
        private readonly ICountryGetterService _countryGetterService;
        public AddressController(IAddressGetterService addressGetterService,IAddressDeleterService addressDeleterService, IAddressUpdaterService addressUpdaterService,IAddressAdderService addressAdderService, ILogger<AddressController> logger, IAccountService accountService, ICountryGetterService countryGetterService)
        {
            _addressAdderService = addressAdderService;
            _addressUpdaterService = addressUpdaterService;
            _addressGetterService = addressGetterService;
            _addressDeleterService = addressDeleterService;
            _accountService = accountService;
            _countryGetterService = countryGetterService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index action method displays all user's addresses");
  
            Guid userId = _accountService.GetUserId(User);
            IEnumerable<AddressResponse> addresses = await _addressGetterService.GetAllAddresses(userId);

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

            Guid userId = _accountService.GetUserId(User);
            AddressResponse? result = await _addressAdderService.AddAddress(addressAddRequest, userId);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Settings", "Account");

        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            _logger.LogDebug("DeleteAddress action method deletes address for specific user. Parameter: id: {id}", addressId);

            if(addressId <= 0)
            {
                return BadRequest();
            }

            Guid userId = _accountService.GetUserId(User);
            bool result = await _addressDeleterService.DeleteAddress(addressId, userId);

            if (!result)
            {
                _logger.LogError("Invalid deletion in DeleteAddress action: id: {id}", addressId);
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditAddress(int addressId)
        {
            _logger.LogDebug("EditAddress action returns edit view with address to edition. Parameter: id: {id}", addressId);

            if (addressId <= 0) {
                return BadRequest();
            }

            Guid userId = _accountService.GetUserId(User);
            AddressResponse? address = await _addressGetterService.GetAddressById(addressId, userId);
            if (address == null)
            {
                _logger.LogWarning("Address not found in EditAddress action.");
                return NotFound();
            }

            IEnumerable<CountryResponse> countries = await _countryGetterService.GetAllCountries();
            ViewBag.Countries = new SelectList(countries,nameof(CountryResponse.Id),nameof(CountryResponse.Name), address.CountryId);

            return View(address.ToUpdateRequest());
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditAddress(AddressUpdateRequest addressUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<CountryResponse> countries = await _countryGetterService.GetAllCountries();
              
                ViewBag.Countries = new SelectList(countries,nameof(CountryResponse.Id)
                ,nameof(CountryResponse.Name),
                addressUpdateRequest.CountryId);

                return View(addressUpdateRequest);
            }

            _logger.LogDebug("[HttpPost]EditAddress action method edits address for specific user. Parameter: addressUpdateRequest: {addressUpdateRequest}", addressUpdateRequest.ToString());

            Guid userId = _accountService.GetUserId(User);
            AddressResponse? response = await _addressUpdaterService.UpdateAddress(addressUpdateRequest, userId);
            if (response == null) 
            {
                return Forbid();
            }

            return RedirectToAction("Index");
        }
    }
}