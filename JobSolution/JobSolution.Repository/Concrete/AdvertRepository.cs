using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Database;
using JobSolution.Infrastructure.Extensions;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(AppDbContext context) : base(context) { }
        public async Task Delete(int advertId)
        {
            var advert = await _dbContext.Adverts.FirstOrDefaultAsync(x => x.Id == advertId);

            _dbContext.Adverts.Remove(advert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Advert> GetAdvertByID(int advertId)
        {
           return await _dbContext.Adverts.Include(x => x.Cities).Include(x => x.User).Include(x => x.User.Profile).Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == advertId);
        }

        public async Task<IQueryable<Advert>> GetAllAdverts() { 
            return  _dbContext.Adverts.Include(x => x.Category).Include(x=>x.Cities).Include(x=>x.User).Include(x=>x.User.Profile).AsQueryable();
        }

        public async Task Add(Advert advert)
        {
            _dbContext.Adverts.Add(advert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Advert advert)
        {
            _dbContext.Adverts.Update(advert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedResult<AdvertDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper, int UserId)
        {

            return await _dbContext.Set<Advert>().Where(x => x.UserId == UserId).CreatePaginatedResultAsync<Advert, AdvertDTO>(pagedRequest, mapper, UserId);
           
        }
    }
}
