using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Database;
using JobSolution.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class TypeJobRepository : Repository<TypeJob>, IJobTypeRepository
    {
        public TypeJobRepository(AppDbContext context) : base(context) { }

        public async Task<IQueryable<TypeJob>> GetTypeJobs()
        {
            
           return _dbContext.TypeJobs;
            
        }
    }
}
