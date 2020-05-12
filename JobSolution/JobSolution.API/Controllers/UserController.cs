using AutoMapper;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IProfileService _userService;
        private readonly IJobService _jobService;
        private readonly IStudentJobService _studentJobService;
        public UserController(IProfileService userService, 
            IJobService jobService,
            IStudentJobService studentJobService
            )
        {
            _userService = userService;
            _jobService = jobService;
            _studentJobService = studentJobService;
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            return Ok(await _userService.GetAuthProfile());
        }

    }
}
