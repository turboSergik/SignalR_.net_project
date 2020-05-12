using JobSolution.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
   public  interface IStudentJobService
   {
        Task Add(int jobId);
        Task Delete(int jobId);
        Task<IList<JobDTO>> GetStudentJobs();
        Task<IList<int>> GetListId();
        Task DeleteStudentJobs(int id);
   }
}

