using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Database;
using JobSolution.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class CategoryRepository : Repository<Categories>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
        public async Task<IQueryable<Categories>> GetCategories()
        {
            return  _dbContext.Categories;
        }
    }
}
