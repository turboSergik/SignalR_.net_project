using JobSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Interfaces
{
    public interface IStudentJobRepository
    {
        Task Add(int UserId, int jobId);
        Task Delete (int UserId ,int jobId);
    }
}
