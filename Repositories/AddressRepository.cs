using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SportsShopDbContext _context;

        public AddressRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<Address> AddAddress(Address model)
        {
            _context.Addresses.Add(model);
            await _context.SaveChangesAsync();
            return model;
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
            int count = await _context.SaveChangesAsync();
            return count > 0;
        }

        public async Task<Address?> GetAddressById(int? id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(address => address.Id == id);
        }

        public async Task<int> GetAddressId(int id)
        {
            return await _context.Addresses.Where(item => item.Id == id).Select(address => address.Id).FirstOrDefaultAsync();
        }

        public IQueryable<Address> GetAllAddresses(Guid userId)
        {
            return _context.Addresses.Where(address => address.UserId == userId && address.IsActive).AsQueryable();
        }

        public async Task UpdateAddress(Address model)
        {
            Address? address = await _context.Addresses.FindAsync(model.Id);

            if (address == null)
            {
                return;
            }

            address.Country = model.Country;
            address.City = model.City;
            address.Street = model.Street;
            address.ZipCode = model.ZipCode;
            await _context.SaveChangesAsync();
        }
    }
}
