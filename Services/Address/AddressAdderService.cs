using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.DTO.AddressDto;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Interface for adding addresses to the database.
    /// </summary>
    public class AddressAdderService : IAddressAdderService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressAdderService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
       
        /// <summary>
        /// Add to the database a new address for the specific user.
        /// </summary>
        /// <param name="model">Address</param>
        /// <param name="userId"></param>
        /// <returns>Address with Id</returns>
        public async Task<AddressResponse?> AddAddress(AddressAddRequest model, Guid userId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null || model == null)
            {
                return null;
            }

            Address address = model.ToAddress(user.Id);
      
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address.ToAddressResponse();
        }
    }
}
