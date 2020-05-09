using AutoMapper;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
    public interface IAdvertServices
    {
        Task<IList<AdvertDTO>> GetAll();
        Task<AdvertDTO> GetByID(int advertId);
        Task Add(AdvertDTO advertDTO);
        Task Update(AdvertDTO advertDTO, int id);
        Task Remove(int advertId);
        Task<PaginatedResult<AdvertDTO>> GetAdvertsForStudent(PagedRequest pagedRequest, IMapper mapper);

    }
}
