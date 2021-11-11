using Microsoft.EntityFrameworkCore;
using SolarCalculator.Data;
using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly AppDbContext _appDbContext;
        public SimulationRepository(
            AppDbContext appDbContext
        )
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Simulation>> GetAsync()
        {
            var simulations =
                await _appDbContext
                .Simulations
                .Include(s => s.Address)
                .ToListAsync();

            return simulations;
        }

        public async Task<Simulation> GetByIdAsync(
            Guid id
        )
        {
            var simulation =
                await _appDbContext
                .Simulations
                .Include(s => s.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            return simulation;
        }

        public async Task<Simulation> PostAsync(
            CreateSimulationViewModel model
        )
        {
            var address = new Address
            {
                City = model.City,
                Complement = model.Complement,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                State = model.State,
                Street = model.Street,
                ZipCode = model.ZipCode
            };

            var generatorCost =
                await _appDbContext
                .GeneratorCosts
                .FirstOrDefaultAsync();

            var KwhCost =
                await _appDbContext
                .KwhCosts
                .FirstOrDefaultAsync(k => k.State.ToLower() == model.State.ToLower());

            var kwhMonthly = model.EnergyCostMonthly / KwhCost.Cost;
            var total = generatorCost.Cost * kwhMonthly;
            var months = (int)Math.Ceiling(total / model.EnergyCostMonthly);

            var simulation = new Simulation
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Address = address,
                GeneratorCost = generatorCost.Cost,
                KwhCost = KwhCost.Cost,
                QuantityKWhMonthly = kwhMonthly,
                Months = months,
                Total = total
            };

            await _appDbContext.Simulations.AddAsync(simulation);
            await _appDbContext.SaveChangesAsync();

            return simulation;
        }
    }
}
