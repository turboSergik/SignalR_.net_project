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
        public UserController(IProfileService userService, IJobService jobService)
        {
            _userService = userService;
            _jobService = jobService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> StudentProfile()
        {
            return Ok(await _userService.GetAuthStudentProfile());
        }

        [HttpGet("EmployerProfile")]
        [Authorize]
        public async Task<IActionResult> EmployerProfile()
        {
            return Ok(await _userService.GetAuthEmployerPofiles());
        }


        [HttpPost("Subscribe/{jobId}")]
        [Authorize(Roles ="Employer" )]
        public async Task<IActionResult> Subscribe([FromRoute] int jobId)
        {
            await _jobService.Subscribe(jobId);
            return Ok();
        }

    }
}
