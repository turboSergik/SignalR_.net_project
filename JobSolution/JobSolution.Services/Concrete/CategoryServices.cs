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
    public class CategoryServices : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IList<CategoryDTO>> GetCategories()
        {
            return _mapper.Map<IQueryable<Categories>, IList<CategoryDTO>>(await _categoryRepository.GetCategories());
        }
    }







}
