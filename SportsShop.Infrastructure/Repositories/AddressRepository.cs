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
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;
            _context.Addresses.Add(model);
            await _context.SaveChangesAsync();
            await _context.Entry(model).Reference(a => a.Country).LoadAsync(); // explicit loading
            return model;
        }

        public async Task<bool> DeleteAddress(Address model)
        {
            model.IsActive = false;
            model.DeleteDate = DateTime.UtcNow;
            int count = await _context.SaveChangesAsync();
            return count > 0;
        }

        public async Task<Address?> GetAddressById(int? id)
        {
            return await _context.Addresses.Include(item => item.Country).FirstOrDefaultAsync(address => address.Id == id);
        }

        public async Task<int> GetAddressId(int id)
        {
            return await _context.Addresses.Where(item => item.Id == id).Select(address => address.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddresses(Guid userId)
        {
            return await _context.Addresses.Include(item => item.Country).Where(address => address.UserId == userId && address.IsActive).ToListAsync();
        }

        public async Task<Address?> UpdateAddress(Address model)
        {
            Address? address = await _context.Addresses.FindAsync(model.Id);
        
            address!.CountryId = model.CountryId;
            address!.City = model.City;
            address!.Street = model.Street;
            address!.ZipCode = model.ZipCode;
            await _context.SaveChangesAsync();
            await _context.Entry(address).Reference(item => item.Country).LoadAsync();
 
            return address;
        }
    }
}
