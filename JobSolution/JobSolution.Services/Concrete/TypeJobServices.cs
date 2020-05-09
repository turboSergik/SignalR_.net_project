using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class TypeJobServices : ITypeJobService
    {

        private readonly IJobTypeRepository _jobTypeRepository;
        private readonly IMapper _mapper;
        public TypeJobServices(IJobTypeRepository jobTypeRepository, IMapper mapper)
        {
            _jobTypeRepository = jobTypeRepository;
            _mapper = mapper;
        }

        public async Task<IList<TypeJobDTO>> GetTypeJobs()
        {
            return _mapper.Map<IQueryable<TypeJob>, IList<TypeJobDTO>>(await _jobTypeRepository.GetTypeJobs());        
        }
    }


}
