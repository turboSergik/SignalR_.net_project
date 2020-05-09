using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class AdvertServices : IAdvertServices
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;

        public AdvertServices(IAdvertRepository advertRepository, IMapper mapper, IHttpContextAccessor context)
        {
            _advertRepository = advertRepository;
            _mapper = mapper;
            _context = context;
        }

        public async  Task Add(AdvertDTO advertDTO)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var advert = _mapper.Map<Advert>(advertDTO);
            advert.UserId = UserId;
         
            await _advertRepository.Add(advert);
        }

        public async Task<IList<AdvertDTO>> GetAll()
        {
            var adverts = await _advertRepository.GetAllAdverts();
            var advertListDTO = _mapper.Map<IQueryable<Advert>, IList<AdvertDTO>>(adverts);

            return advertListDTO;
        }

        public async Task<AdvertDTO> GetByID(int advertId)
        {
            var foundAdvert = await _advertRepository.GetAdvertByID(advertId);
            return _mapper.Map<AdvertDTO>(foundAdvert);
        }

        public async Task<PaginatedResult<AdvertDTO>> GetAdvertsForStudent(PagedRequest pagedRequest, IMapper mapper)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var result = await _advertRepository.GetPagedData(pagedRequest, mapper, UserId);
            return result;
        }

        public async Task Remove(int advertId)
        {
            await _advertRepository.Delete(advertId);
        }

        public async Task Update(AdvertDTO advertDTO, int id)
        {
            var userId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var dbentity = await _advertRepository.GetAdvertByID(id);

            dbentity.CategoryId = advertDTO.CategoryId;
            dbentity.CityId = advertDTO.CityId;
            dbentity.Contact = advertDTO.Contact;
            dbentity.Id = id;
            dbentity.UserId = userId;
            dbentity.Title = advertDTO.Title;
            dbentity.Description = advertDTO.Description;
            await _advertRepository.Update(dbentity);
        }

        
    }
}
