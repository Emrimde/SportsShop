using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces.IAddress;

namespace Services.IAddress
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressGetterService : IAddressGetterService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressGetterService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
        public async Task<Address> GetAddress(int? id)
        {
            if (id == null)
            {
                return null!;
            }
            Address? address = await _context.Addresses.FirstOrDefaultAsync(item => item.Id == id);
            if (address == null)
            {
                return null!;
            }
            return address;


        }

        public async Task<List<Address>> ShowAddresses(Guid userId)
        {
            return await _context.Addresses.Where(item => item.UserId == userId && item.IsActive).ToListAsync();
        }
    }
}
