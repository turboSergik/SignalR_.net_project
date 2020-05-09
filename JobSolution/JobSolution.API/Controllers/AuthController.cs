using AutoMapper;
using JobSolution.Domain;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using JobSolution.Infrastructure.Configuration;
using JobSolution.Infrastructure.Database;
using JobSolution.Infrastructure.Extensions;
using JobSolution.Services.Concrete;
using JobSolution.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobSolution.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
            
        private readonly AuthOptions _authOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IOptions<AuthOptions> authOption, SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext dbContext,
            IAuthService authService
            )
        {
            _authOptions = authOption.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _authService = authService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userLoginDto)
        {
            var checkPassword = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            var role = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim("UserId", user.Id.ToString()));

            foreach (var item in role)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            
            if (checkPassword.Succeeded)
            {
                var signinCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authOptions.Issuer,
                     audience: _authOptions.Audience,
                     claims: claims,
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials);
                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);
                return Ok(new { AccessToken = encodedToken });
            }
            return Unauthorized();
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Add()
        {
            return await _authService.AddUser();
        }
    }
}