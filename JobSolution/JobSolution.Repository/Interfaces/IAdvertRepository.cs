using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Interfaces
{
    public interface IAdvertRepository
    {
        Task<IQueryable<Advert>> GetAllAdverts();
        Task<Advert> GetAdvertByID(int advertId);
        Task Update(Advert advert);
        Task Delete(int advertId);
        Task Add(Advert advert);
        Task<PaginatedResult<AdvertDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper, int UserId);
    }
}
