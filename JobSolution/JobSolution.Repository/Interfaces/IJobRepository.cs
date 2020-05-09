using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Interfaces
{
    public interface IJobRepository
    {
        Task<IQueryable<Job>> GetAllJobs();
        Task<Job> GetJobByID(int JobId);
        Task Update(Job job);
        Task Delete(int job);
        Task Add(Job job);
        Task<bool> SaveAll();
        Task<Job> GetByIdWithInclude(int id, params Expression<Func<Job, object>>[] includeProperties);
        Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper);
        Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper, int UserId);
        Task<PaginatedResult<JobDTO>> GetPagedDataStudent(PagedRequest pagedRequest, IMapper mapper, int UserId);
        Task<PaginatedResult<JobDTO>> GetPagedDataByType(PagedRequest pagedRequest, IMapper mapper, int typeId);
    }
}
