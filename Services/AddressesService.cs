using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;
using ServiceContracts.Interfaces;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressesService : IAddressesService
    {
        private readonly SportsShopDbContext _context;
        private readonly UserManager<User> _userManager;

        public AddressesService(SportsShopDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
        public async Task<bool> AddAddress(AddressDTO model, string UserId)
        {
            User? user = await _userManager.FindByIdAsync(UserId.ToString());

            if (user == null)
            {
                return false;
            }

            Address address = new Address
            {
                UserId = user.Id,
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                ZipCode = model.ZipCode,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<bool> EditAddress(AddressDTO model)
        {
            Address? address = await GetAddress(model.Id);
            if (address == null)
            {
                return false;
            }
            address.Country = model.Country;
            address.City = model.City;
            address.Street = model.Street;
            address.ZipCode = model.ZipCode;
            address.EditDate = DateTime.UtcNow;
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetAddress(int id)
        {
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
