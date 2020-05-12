using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
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
   public class StudentJobRepository : Repository<StudentJobs>, IStudentJobRepository
   {
        public StudentJobRepository(AppDbContext context) : base(context) { }

        public async Task Add(int UserId, int jobId)
        {
             _dbContext.StudentJobs.Add(new StudentJobs { UserId = UserId, JobId = jobId });
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int UserId, int jobId)
        {
            var entity = _dbContext.StudentJobs.Where(x => x.JobId == jobId && x.UserId == UserId).FirstOrDefault();
            _dbContext.StudentJobs.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<StudentJobs>> GetAll(int UserId)
        {
            return _dbContext.StudentJobs.Where(x => x.UserId == UserId); 
        }

        public async Task<IList<int>> GetStudentJobs(int UserId)
        {
            return  _dbContext.StudentJobs.Where(x => x.UserId == UserId).Select(x => x.JobId).ToList();
        }

    }
}
