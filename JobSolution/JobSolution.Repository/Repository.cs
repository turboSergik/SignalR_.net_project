using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Database;
using JobSolution.Infrastructure.Extensions;
using JobSolution.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByID(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Remove(int Id)
        {
            var temporaryEntity = await _dbContext.Set<T>().FindAsync(Id);
            if (temporaryEntity != null)
            {
                _dbContext.Set<T>().Remove(temporaryEntity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveAll()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


    }
}
