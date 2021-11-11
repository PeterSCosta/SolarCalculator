using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public interface IGeneratorCostRepository
    {
        Task<List<GeneratorCost>> GetAsync();

        Task<GeneratorCost> GetByIdAsync(
            Guid id
        );

        Task<GeneratorCost> PostAsync(
            CreateGeneratorCostViewModel model
        );

        Task<GeneratorCost> PutAsync(
            Guid id,
            CreateGeneratorCostViewModel model
        );

        Task DeleteAsync(
            Guid id
        );
    }
}