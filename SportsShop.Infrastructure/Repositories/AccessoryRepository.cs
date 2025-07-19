using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class AccessoryRepository : IAccessoryRepository
    {
        private readonly SportsShopDbContext _context;

        public AccessoryRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<dynamic>> FilterAccessory(string type)
        {
            if (type == "GymnasticRing")
            {
                var rings = await _context.GymnasticRings
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();

                return rings.Cast<dynamic>().ToList();
            }
            if (type == "TrainingRubber")
            {
                var rubbers = await _context.TrainingRubbers
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();
                return rubbers.Cast<dynamic>().ToList();
            }
            if (type == "WeightPlate")
            {
                var plates = await _context.WeightPlates
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();
                return plates.Cast<dynamic>().ToList();
            }
            return new List<dynamic>();
        }

        public Task<dynamic> GetObject(int id)
        {
            throw new NotImplementedException();
        }
    }
}
