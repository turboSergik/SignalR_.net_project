using AutoMapper;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertServices _advertService;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public AdvertController(IAdvertServices advertServices, ICategoryService categoryService, ICityService cityService, IMapper mapper)
        {
            _advertService = advertServices;
            _categoryService = categoryService; ;
            _cityService = cityService;
            _mapper = mapper;
        }

       
        [HttpGet("{advertId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int advertId)
        {
            var obj = await _advertService.GetByID(advertId);
            return Ok(obj);
        }

        [HttpPut("Update/{advertId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody]AdvertDTO advert, int advertId)
        {
            if (ModelState.IsValid)
            {
                await _advertService.Update(advert, advertId);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Post([FromBody] AdvertDTO advertDTO)
        {
            if (ModelState.IsValid)
            {
                await _advertService.Add(advertDTO);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{advertId}")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> Delete(int advertId)
        {
            await _advertService.Remove(advertId);
            return Ok();
        }

        [HttpPost("Student/Adverts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPageForTableEmployer([FromBody] PagedRequest pagedRequest)
        {
            var result = await _advertService.GetAdvertsForStudent(pagedRequest, _mapper);
            return Ok(result);
        }
    }
}
