using Microsoft.EntityFrameworkCore;
using SolarCalculator.Data;
using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public class KwhCostRepository : IKwhCostRepository
    {
        private readonly AppDbContext _appDbContext;
        public KwhCostRepository(
            AppDbContext appDbContext
        )
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<KwhCost>> GetAsync()
        {
            var kwhCosts =
                await _appDbContext
                .KwhCosts
                .AsNoTracking()
                .ToListAsync();

            return kwhCosts;
        }

        public async Task<KwhCost> GetByIdAsync(
            Guid id
        )
        {
            var kwhCost =
                await _appDbContext
                .KwhCosts
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);

            return kwhCost;
        }

        public async Task<KwhCost> GetByStateAsync(
            string state
        )
        {
            var kwhCost =
                await _appDbContext
                .KwhCosts
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.State.ToLower() == state.ToLower());

            return kwhCost;
        }
        public async Task<KwhCost> PostAsync(
            CreateKwhCostViewModel model
        )
        {
            var kwhCost = new KwhCost
            {
                State = model.State,
                Cost = model.Cost
            };

            await _appDbContext.KwhCosts.AddAsync(kwhCost);
            await _appDbContext.SaveChangesAsync();

            return kwhCost;
        }

        public async Task<KwhCost> PutAsync(
            Guid id,
            EditKwhCostViewModel model
        )
        {
            var kwhCost = await GetByIdAsync(id);

            if (kwhCost == null)
            {
                throw new ArgumentException(); ;
            }

            kwhCost.Cost = model.Cost;

            _appDbContext.KwhCosts.Update(kwhCost);
            await _appDbContext.SaveChangesAsync();

            return kwhCost;
        }

        public async Task DeleteAsync(
            Guid id
        )
        {
            var kwhCost = await GetByIdAsync(id);

            if (kwhCost == null)
            {
                throw new ArgumentException(); ;
            }

            _appDbContext.KwhCosts.Remove(kwhCost);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
