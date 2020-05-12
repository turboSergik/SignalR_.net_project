using JobSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using JobSolution.Repository;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using AutoMapper;

namespace JobSolution.Services.Interfaces
{
    public interface IJobService
    {
        Task<IList<JobDTO>> GetAll();
        Task<JobDTO> GetByID(int JobId);
        Task Add(/*JobDTO JobDTO*/);
        Task Update(int id);
        Task Remove(int JobId);
        Task<IList<JobDTO>> GetByType(int TypeId);
        Task<IList<JobDTO>> GetJobsByCategory(string Category);
        Task AddedJobByStudent(int id);
        Task DeleteJobStudent(int id);
        Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper);
        Task<PaginatedResult<JobDTO>> GetJobsForEmployer(PagedRequest pagedRequest, IMapper mapper);
        Task<PaginatedResult<JobDTO>> GetJobsForStudent(PagedRequest pagedRequest, IMapper mapper);
        Task<PaginatedResult<JobDTO>> GetJobsByType(PagedRequest pagedRequest, IMapper mapper, int typeId);
    }
}