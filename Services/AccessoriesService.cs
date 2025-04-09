using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccessoriesService : IAccessoriesService
    {
        private readonly SportsShopDbContext _context;
        public AccessoriesService(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<GymnasticRing>> GetAllGymnasticRings()
        {
            return await _context.GymnasticRings.Include(item => item.Product).Where(item=> item.Product.IsActive).ToListAsync();
        }

        public async Task<List<TrainingRubber>> GetAllTrainingRubbers()
        {
            return await _context.TrainingRubbers.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }

        public async Task<List<WeightPlate>> GetAllWeightPlates()
        {
            return await _context.WeightPlates.Include(item => item.Product).Where(item => item.Product.IsActive).ToListAsync();
        }
    }
}
