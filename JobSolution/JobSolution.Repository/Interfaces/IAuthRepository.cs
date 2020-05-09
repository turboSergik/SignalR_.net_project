using JobSolution.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUser(string username);
        Task AddUser(User user);
    }
}
