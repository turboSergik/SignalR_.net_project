using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository ,  IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        public async Task<IList<CityDTO>> GetCities()
        {
            return _mapper.Map<IQueryable<Cities>, IList<CityDTO>>(await _cityRepository.GetCities());
        }
    }
}
