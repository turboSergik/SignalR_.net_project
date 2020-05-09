using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
   public  interface IStudentJobService
   {
        Task Add( int jobId);
        Task Delete(int jobId);
   }
}

