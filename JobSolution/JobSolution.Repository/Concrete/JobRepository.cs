using AutoMapper;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Database;
using JobSolution.Infrastructure.Extensions;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context) { }
        public async Task Delete(int JobId)
        {
            var Job = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == JobId);

            _dbContext.Jobs.Remove(Job);
             await _dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Job>> GetJobs()
        {
            return _dbContext.Jobs;
        }

        public async Task<IQueryable<Job>> GetAllJobs()
        {
            return _dbContext.Jobs.Include(x => x.Category).Include(x=>x.Cities).Include(x=>x.User).Include(x=>x.User.Profile).Include(x=>x.TypeJob).AsQueryable();
        }

        public async Task<Job> GetJobByID(int JobId)
        {
            return await _dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == JobId);
        }
        
        public async Task Update(Job job)
        {

           _dbContext.Jobs.Update(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(Job job)
        {
            _dbContext.Jobs.Add(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }


        public async Task<Job> GetByIdWithInclude(int id, params Expression<Func<Job, object>>[] includeProperties) 
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        private IQueryable<Job> IncludeProperties(params Expression<Func<Job, object>>[] includeProperties) 
        {
            IQueryable<Job> entities = _dbContext.Set<Job>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
        public async Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper)
        {
            return await _dbContext.Set<Job>().CreatePaginatedResultAsync<Job, JobDTO>(pagedRequest, mapper);
        }

        public async Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper, int UserId)
        {
            var result = _dbContext.Set<Job>().Where(x => x.UserId == UserId).CreatePaginatedResultAsync<Job, JobDTO>(pagedRequest, mapper, UserId);
            
            return await result;
        }

        public async Task<PaginatedResult<JobDTO>> GetPagedDataStudent(PagedRequest pagedRequest, IMapper mapper, int UserId)
        {
            var result = _dbContext.Set<Job>().Where(x => x.UserId == UserId).CreatePaginatedResultAsync<Job, JobDTO>(pagedRequest, mapper, UserId);
            return await result;
        }

        public async Task<PaginatedResult<JobDTO>> GetPagedDataByType(PagedRequest pagedRequest, IMapper mapper, int typeId)
        {
            var result = _dbContext.Set<Job>().Where(x => x.TypeJobId == typeId).CreatePaginatedResultAsync<Job, JobDTO>(pagedRequest, mapper, typeId);

            return await result;
        }


    }
}
