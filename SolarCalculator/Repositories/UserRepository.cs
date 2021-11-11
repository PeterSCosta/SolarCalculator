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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(
            AppDbContext appDbContext
        )
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetAsync()
        {
            var users = await _appDbContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetByIdAsync(
            Guid id
        )
        {
            var user =
                await _appDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetByUsernameAsync(
            string username,
            string password
        )
        {
            var user = await _appDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.UserName.ToLower() == username.ToLower()
                    && u.Password == password
                );

            return user;
        }

        public async Task<User> PostAsync(
            CreateUserViewModel model
        )
        {
            var user = new User
            {
                UserName = model.Username,
                Password = model.Password,
                Role = RoleEnum.admin.ToString()
            };

            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            return user;
        }
    }
}
