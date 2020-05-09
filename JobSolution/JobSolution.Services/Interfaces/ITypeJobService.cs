using JobSolution.DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
    public interface ITypeJobService
    {
        Task<IList<TypeJobDTO>> GetTypeJobs();
    }

}
