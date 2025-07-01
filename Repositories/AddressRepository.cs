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

        public async Task<Address?> AddAddress(Address model, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAddress(int id)
        {
            Address? address = await _context.Addresses.FindAsync(id);
            address!.IsActive = false;
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
            return _context.Addresses.Where(address => address.UserId == userId).AsQueryable();
        }

        public async Task UpdateAddress(Address model)
        {
            Address? address = await _context.Addresses.FindAsync(model.Id);
            address!.Country = model.Country;
            address.City = model.City;
            address.Street = model.Street;
            address.ZipCode = model.ZipCode;
            await _context.SaveChangesAsync();
        }
    }
}
