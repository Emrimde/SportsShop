using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class TrainingRubberRepository : ITrainingRubberRepository
    {
        private readonly SportsShopDbContext _context;

        public TrainingRubberRepository(SportsShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<TrainingRubber> GetAllTrainingRubbers()
        {
            return _context.TrainingRubbers.Include(item => item.Product).Where(item => item.Product.IsActive);
        }

        public async Task<TrainingRubber?> GetTrainingRubberById(int id)
        {
            return await _context.TrainingRubbers
                .Include(item => item.Product)
                .FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
        }
    }
}
