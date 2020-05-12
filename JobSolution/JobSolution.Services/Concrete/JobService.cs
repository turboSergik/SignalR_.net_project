using AutoMapper;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Pagination;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class JobService : IJobService 
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IStudentJobService _studentJobService;

        public JobService(IJobRepository jobRepository, 
            IMapper mapper, IHttpContextAccessor context, 
            IHostingEnvironment hostingEnvironment,
            IStudentJobService studentJobService)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _studentJobService = studentJobService;
        }

        public async Task Add()
        {
            var entity = new JobDTO();
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var job = new Job();
            try
            {
                foreach (var key in _context.HttpContext.Request.Form.Keys)
                {
                    entity = JsonConvert.DeserializeObject<JobDTO>(_context.HttpContext.Request.Form[key]);
                    job = _mapper.Map<Job>(entity);
                    var file = _context.HttpContext.Request.Form.Files.Count > 0 ? _context.HttpContext.Request.Form.Files[0] : null;
                    if (file != null)
                    {
                        
                        string folderName = "Upload";
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        if (string.IsNullOrWhiteSpace(webRootPath))
                        {
                            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        string newPath = Path.Combine(webRootPath, folderName);

                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        if (file.Length > 0)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            string fullPath = Path.Combine(newPath, fileName);
                            job.ImagePath = fullPath;
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }


            
            job.UserId = UserId;
            job.PostDate = DateTime.Now;

            await _jobRepository.Add(job);
            _jobRepository.SaveAll();

        }

        public async Task<IList<JobDTO>> GetAll()
        {
            var Jobs = await _jobRepository.GetAllJobs();
            var JobsListDTO = _mapper.Map<IQueryable<Job>, IList<JobDTO>>(Jobs);
            foreach (var item in JobsListDTO)
            {
                try
                {

                    byte[] b = File.ReadAllBytes(item.ImagePath);
                    item.ImagePath = "data:image/png;base64," + Convert.ToBase64String(b);
                }
                catch
                {

                }
            }
            return JobsListDTO;
        }
        public async Task<JobDTO> GetByID(int id)
        {
            var toReturn = await _jobRepository.GetJobByID(id);
            return _mapper.Map<JobDTO>(toReturn);
        }
        
        public async Task Update(int id) {

            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var dbjob = await _jobRepository.GetJobByID(id);
            JobDTO job = null;

            try
            {
                foreach (var key in _context.HttpContext.Request.Form.Keys)
                {
                    job = JsonConvert.DeserializeObject<JobDTO>(_context.HttpContext.Request.Form[key]);
                    var file = _context.HttpContext.Request.Form.Files.Count > 0 ? _context.HttpContext.Request.Form.Files[0] : null;
                    if (file != null)
                    {

                        string folderName = "Upload";
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        if (string.IsNullOrWhiteSpace(webRootPath))
                        {
                            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        string newPath = Path.Combine(webRootPath, folderName);

                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        if (file.Length > 0)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            string fullPath = Path.Combine(newPath, fileName);
                            dbjob.ImagePath = fullPath;
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

           
            dbjob.TypeJobId = job.TypeJobId;
            dbjob.CityId = job.CityId;
            dbjob.Contact = job.Contact;
            dbjob.EndDate = job.FinishedOn;
            dbjob.Id = id;
            dbjob.UserId = UserId;
            dbjob.Title = job.Title;
            dbjob.Salary = job.Salary;
            dbjob.CategoryId = job.CategoryId;
            await _jobRepository.Update(dbjob);
        
        }
        public async Task Remove(int JobId) {
            
            await _jobRepository.Delete(JobId);
        }

        public async Task<IList<JobDTO>> GetJobsByCategory(string category)
        {
            var result = await GetAll();
            return result.Where(x => x.Category == category).ToList();
          
        }

        public async Task<PaginatedResult<JobDTO>> GetPagedData(PagedRequest pagedRequest, IMapper mapper) 
        {
            var result = await _jobRepository.GetPagedData(pagedRequest, mapper);
            foreach (var item in result.Items)
            {
                try
                {

                    byte[] b = System.IO.File.ReadAllBytes(item.ImagePath);
                    item.ImagePath = "data:image/png;base64," + Convert.ToBase64String(b);
                }
                catch
                {

                }
            }

            return result;

        }

        public async Task<PaginatedResult<JobDTO>> GetJobsForEmployer(PagedRequest pagedRequest, IMapper mapper)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var result = await _jobRepository.GetPagedData(pagedRequest, mapper, UserId);
            return  result;
        }

        public async Task<PaginatedResult<JobDTO>> GetJobsForStudent(PagedRequest pagedRequest, IMapper mapper)
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var ListJobsForStudents = _studentJobService.GetListId();

            var result = await _jobRepository.GetPagedDataStudent(pagedRequest, mapper, UserId);
            return result;
        }

        public async Task<IList<JobDTO>> GetByType(int TypeId)
        {
            var AllJobs = await _jobRepository.GetAllJobs();


            var JobsList = AllJobs.Where(x=>x.TypeJobId==TypeId);

            var result = _mapper.Map<IQueryable<Job>, IList<JobDTO>>(JobsList);

            return result;
        }

        public async Task<PaginatedResult<JobDTO>> GetJobsByType(PagedRequest pagedRequest, IMapper mapper, int typeId)
        {
            var result = await _jobRepository.GetPagedDataByType(pagedRequest, mapper, typeId);
            return result;
        }

        

        public async Task AddedJobByStudent(int id)
        {
            await _studentJobService.Add(id);
        }

        public async Task DeleteJobStudent(int id)
        {
            await _studentJobService.Delete(id);
        }
    }
}
