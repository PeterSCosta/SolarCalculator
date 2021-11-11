using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public interface ISimulationRepository
    {
        Task<List<Simulation>> GetAsync();

        Task<Simulation> GetByIdAsync(
            Guid id
        );

        Task<Simulation> PostAsync(
            CreateSimulationViewModel model
        );
    }
}