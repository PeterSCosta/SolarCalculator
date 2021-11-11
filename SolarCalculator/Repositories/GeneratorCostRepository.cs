using Microsoft.EntityFrameworkCore;
using SolarCalculator.Data;
using SolarCalculator.Enums;
using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public class GeneratorCostRepository : IGeneratorCostRepository
    {
        private readonly AppDbContext _appDbContext;
        public GeneratorCostRepository(
            AppDbContext appDbContext
        )
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<GeneratorCost>> GetAsync()
        {
            var generatorCosts =
                await _appDbContext
                .GeneratorCosts
                .AsNoTracking()
                .ToListAsync();

            return generatorCosts;
        }

        public async Task<GeneratorCost> GetByIdAsync(
            Guid id
        )
        {
            var generatorCost =
                await _appDbContext
                .GeneratorCosts
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            return generatorCost;
        }

        public async Task<GeneratorCost> PostAsync(
            CreateGeneratorCostViewModel model
        )
        {
            var generatorCost = new GeneratorCost
            {
                Cost = model.Cost
            };

            await _appDbContext.GeneratorCosts.AddAsync(generatorCost);
            await _appDbContext.SaveChangesAsync();

            return generatorCost;
        }

        public async Task<GeneratorCost> PutAsync(
            Guid id,
            CreateGeneratorCostViewModel model
        )
        {
            var generatorCost = await GetByIdAsync(id);

            if (generatorCost == null)
            {
                throw new ArgumentException();
            }

            generatorCost.Cost = model.Cost;

            _appDbContext.GeneratorCosts.Update(generatorCost);
            await _appDbContext.SaveChangesAsync();

            return generatorCost;
        }

        public async Task DeleteAsync(
            Guid id
        )
        {
            var generatorCost = await GetByIdAsync(id);

            if (generatorCost == null)
            {
                throw new ArgumentException();
            }

            _appDbContext.GeneratorCosts.Remove(generatorCost);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
