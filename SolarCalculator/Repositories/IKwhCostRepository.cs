using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public interface IKwhCostRepository
    {
        Task<List<KwhCost>> GetAsync();

        Task<KwhCost> GetByIdAsync(
            Guid id
        );

        Task<KwhCost> GetByStateAsync(
            string state
        );

        Task<KwhCost> PostAsync(
            CreateKwhCostViewModel model
        );

        Task<KwhCost> PutAsync(
            Guid id,
            EditKwhCostViewModel model
        );

        Task DeleteAsync(
            Guid id
        );
    }
}