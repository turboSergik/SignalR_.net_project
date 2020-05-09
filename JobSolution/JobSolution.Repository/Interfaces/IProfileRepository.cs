using JobSolution.Domain;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Interfaces
{
    public interface IProfileRepository 
    {
        Task<Profile> GetAuthUserProfile(int UserId);
    }
}
