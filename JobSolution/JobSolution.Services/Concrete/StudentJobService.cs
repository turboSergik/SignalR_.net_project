using AutoMapper;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class StudentJobService : IStudentJobService
    {

        private readonly IStudentJobRepository _studentJobRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        private readonly IJobRepository _jobRepository;
        public StudentJobService(IStudentJobRepository studentJobRepository, 
            IMapper mapper, 
            IHttpContextAccessor httpContext,
            IJobRepository jobRepository)
        {
            _studentJobRepository = studentJobRepository;
            _mapper = mapper;
            _context = httpContext;
            _jobRepository = jobRepository;
        }

        public async Task Add(int jobId)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            await _studentJobRepository.Add(UserId, jobId);
        }

        public async Task Delete(int jobId)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
           await  _studentJobRepository.Delete(UserId, jobId);
            
        }

        public Task<IList<int>> GetListId()
        {
            return null;
        }

        public async Task<IList<JobDTO>> GetStudentJobs()
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);

            var JobsList = _jobRepository.GetAllJobs().Result.Where(x => x.UserId == UserId);
            var result = _mapper.Map<IQueryable<Job>, IList<JobDTO>>(JobsList);
            return result;
        }

        public async Task DeleteStudentJobs(int id)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            _studentJobRepository.Delete(UserId,id);
        }
    }
}
