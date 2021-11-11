using SolarCalculator.Models;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarCalculator.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAsync();

        Task<User> GetByIdAsync(
            Guid id
        );

        Task<User> GetByUsernameAsync(
            string username,
            string password
        );

        Task<User> PostAsync(
            CreateUserViewModel model
        );
    }
}