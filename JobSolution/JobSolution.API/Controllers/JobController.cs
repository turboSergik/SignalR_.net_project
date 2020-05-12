using AutoMapper;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;
        private readonly ITypeJobService _typeJobService;
        private readonly IStudentJobService _studentJobService;
        private readonly IMapper _mapper;

        public JobController(IJobService repositoryJob,ICategoryService categoryService, 
            ICityService cityService, 
            ITypeJobService typeJobService,
            IStudentJobService studentJobService,
            IMapper mapper)
        {
            _cityService = cityService;
            _categoryService = categoryService;
            _jobService = repositoryJob;
            _mapper = mapper;
            _typeJobService = typeJobService;
            _studentJobService = studentJobService;
        }

        [HttpGet("Categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategries()
        {
            return Ok(await _categoryService.GetCategories());
        }


        [HttpGet("City")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _cityService.GetCities());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var JobsFromRepo = _jobService.GetAll().Result.ToList();
           
            return Ok(JobsFromRepo);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _jobService.GetByID(id);
            return Ok(obj);
        }

        [HttpPost("Type/{TypeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByType([FromBody] PagedRequest pagedRequest, int TypeId)
        {
            var jobsType = await _jobService.GetJobsByType(pagedRequest,_mapper, TypeId);
            return Ok(jobsType);
        }

        [HttpGet("Types")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypes()
        {
            var jobs = await  _typeJobService.GetTypeJobs();
            return Ok(jobs);
        }



        [HttpPost("Post")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Post()
        {
            if (ModelState.IsValid)
            {
                await _jobService.Add();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobService.Remove(id);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles ="Employer")]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            if (ModelState.IsValid)
            {
                await _jobService.Update(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Category/{category}")]
        [AllowAnonymous]
        public async Task<IList<JobDTO>> GetJobsByCategory(string category)
        {
            return await _jobService.GetJobsByCategory(category);
        }

        [HttpPost("Profile")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetPageForTableEmployer([FromBody] PagedRequest pagedRequest)
        {
            var result = await _jobService.GetJobsForEmployer(pagedRequest, _mapper);
            return Ok(result);
        }

        [HttpPost("Profile/Student")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetPageForTableStudent([FromBody] PagedRequest pagedRequest)
        {
            var result = await _jobService.GetJobsForStudent(pagedRequest, _mapper);
            return Ok(result);
        }

        [HttpPost("PagePerTable")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPageForTable([FromBody] PagedRequest pagedRequest)
        {
            var result = await _jobService.GetPagedData(pagedRequest, _mapper);

            return Ok(result);
        }


        [HttpGet("Added/{jobId}")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> AddedJobStudent([FromRoute]int jobId)
        {
            await  _jobService.AddedJobByStudent(jobId);
            return Ok();
        }

        [HttpDelete("Student/Delete/{jobId}")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> DeleteStudentJobs(int jobId)
        {
             await _jobService.DeleteJobStudent(jobId);
            return Ok();
        }

    }
}