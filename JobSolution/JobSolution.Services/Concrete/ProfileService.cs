using AutoMapper;
using JobSolution.DTO.DTO;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Concrete
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;

        public ProfileService(IProfileRepository profileRepository,IMapper mapper, IHttpContextAccessor context)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _context = context;
        }

        
        public async Task<EmployerProfileDTO> GetAuthEmployerPofiles()
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var UserProfile = await _profileRepository.GetAuthUserProfile(UserId);

            return _mapper.Map<EmployerProfileDTO>(UserProfile);         
        }

        public async  Task<StudentProfileDTO> GetAuthStudentProfile()
        {
            var UserId = Convert.ToInt32(_context.HttpContext.User.Claims.Where(x => x.Type == "UserId").First().Value);
            var UserProfile = await _profileRepository.GetAuthUserProfile(UserId);

            return _mapper.Map<StudentProfileDTO>(UserProfile);
        }
    }
}
