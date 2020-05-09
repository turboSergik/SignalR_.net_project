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
    public class CityRepository : Repository<Cities>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context) { }
        public async Task<IQueryable<Cities>> GetCities()
        {
            return _dbContext.Cities;
        }
    }
}
