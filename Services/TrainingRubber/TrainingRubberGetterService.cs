using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.Interfaces.ITrainingRubber;

namespace Services.TrainingRubber
{
    public class TrainingRubberGetterService : ITrainingRubberGetterService
    {
        private readonly SportsShopDbContext _context;

        public TrainingRubberGetterService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingRubberResponse>> GetAllTrainingRubbers()
        {
            return await _context.TrainingRubbers
                .Include(item => item.Product)
                .Where(item => item.Product.IsActive)
                .Select(item => item.ToTrainingRubberResponse())
                .ToListAsync();
        }

        public async Task<TrainingRubberResponse?> GetTrainingRubberById(int id)
        {
            return await _context.TrainingRubbers
                .Include(item => item.Product)
                .Where(item => item.Product.IsActive && item.ProductId == id)
                .Select(item => item.ToTrainingRubberResponse())
                .FirstOrDefaultAsync();
        }
    }
}
