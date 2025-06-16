using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressDeleterService : IAddressDeleterService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressDeleterService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
        public async Task<bool> DeleteAddress(int id)
        {
            Address? address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return false;
            }
            address.IsActive = false;
            address.DeleteDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
