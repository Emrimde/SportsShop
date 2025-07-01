using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces.IAccessory;

namespace Services.Accessory
{
    public class AccessoryGetterService : IAccessoryGetterService
    {
        private readonly SportsShopDbContext _context;
        public AccessoryGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<dynamic>> FilterAccessory(string type)
        {
            if (type == "GymnasticRings")
            {

                var rings = await _context.GymnasticRings
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();

                return rings.Cast<dynamic>().ToList();
            }
            if (type == "RubberBand")
            {
                var rubbers = await _context.TrainingRubbers
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();
                return rubbers.Cast<dynamic>().ToList();
            }
            if (type == "Weights")
            {
                var plates = await _context.WeightPlates
                    .Include(item => item.Product)
                    .Where(item => item.Product.IsActive)
                    .ToListAsync();
                return plates.Cast<dynamic>().ToList();
            }
            return new List<dynamic>();
        }

        public async Task<dynamic> GetObject(int id)
        {
            var obiekt = await _context.Products.FirstOrDefaultAsync(item => item.Id == id);

            if (obiekt == null)
            {
                return null!;
            }
            return obiekt;
        }
    }
}
