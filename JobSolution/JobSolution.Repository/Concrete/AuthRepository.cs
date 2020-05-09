using JobSolution.Domain.Auth;
using JobSolution.Infrastructure.Database;
using JobSolution.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class AuthRepository:  IAuthRepository
    {
        private readonly AppDbContext _dbContext;
        public AuthRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task AddUser(User user)
        {
            await  _dbContext.Set<User>().AddAsync(user);
        }

        public async Task<User> GetUser(string username)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
