﻿using Entities.DatabaseContext;
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

        public async Task<GymnasticRing> GetGymnasticRing(int id)
        {
            GymnasticRing? gymnasticRing = await _context.GymnasticRings.Include(item=>item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
            if (gymnasticRing == null)
            {
                return null!;
            }
            return gymnasticRing;
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

        public async Task<TrainingRubber> GetTrainingRubber(int id)
        {
            TrainingRubber? trainingRubber = await _context.TrainingRubbers.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
            if (trainingRubber == null)
            {
                return null!;
            }
            return trainingRubber;
        }

        public async Task<WeightPlate> GetWeightPlate(int id)
        {
            WeightPlate? weightPlate = await _context.WeightPlates.Include(item => item.Product).FirstOrDefaultAsync(item => item.ProductId == id && item.Product.IsActive);
            if (weightPlate == null)
            {
                return null!;
            }
            return weightPlate;
        }
    }
}
