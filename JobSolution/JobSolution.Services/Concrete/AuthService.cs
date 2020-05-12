using AutoMapper;
using JobSolution.Domain;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Configuration;
using JobSolution.Infrastructure.Database;
using JobSolution.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository; 
        private readonly AuthOptions _authOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly HttpResponseMessage _responseMessage;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AuthService(AppDbContext dbContext, IAuthRepository authRepository, IOptions<AuthOptions> authOption, SignInManager<User> signInManager, 
            UserManager<User> userManager, IHttpContextAccessor context, IHostingEnvironment hostingEnvironment)
        {
            _authRepository = authRepository;
            _authOptions = authOption.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;

            _context = context;
            _hostingEnvironment = hostingEnvironment;
        } 
        public async Task<IActionResult> AddUser()
        {
            UserRegisterDto userRegisterDto = new UserRegisterDto();
            JobSolution.Domain.Entities.Profile userProfile = new Domain.Entities.Profile();
            try
            {
                foreach (var key in _context.HttpContext.Request.Form.Keys)
                {
                    userRegisterDto = JsonConvert.DeserializeObject<UserRegisterDto>(_context.HttpContext.Request.Form[key]);
            
                    var file = _context.HttpContext.Request.Form.Files.Count > 0 ? _context.HttpContext.Request.Form.Files[0] : null;
                    if (file != null)
                    {

                        string folderName = "Profile";
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
                            userProfile.ImagePath = fullPath;
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

            if(userRegisterDto == null)
            {
                return new StatusCodeResult(500);
            }

            var findExistedProfile = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            if (findExistedProfile != null)
            {
                return new BadRequestObjectResult("Email or username exist!");
            }

            var CreateUserToAdd = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email
            };


            await _userManager.CreateAsync(CreateUserToAdd, userRegisterDto.Password);
            await _userManager.AddToRoleAsync(CreateUserToAdd, userRegisterDto.RoleFromRegister);
            var AddUser = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email
            };
            var Profile = new JobSolution.Domain.Entities.Profile
            {

                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                PhoneNumber = userRegisterDto.PhoneNumber,
                University = userRegisterDto.University,
                UserId = _userManager.FindByEmailAsync(AddUser.Email).Result.Id,

            };

            _dbContext.Profiles.Add(Profile);
            _dbContext.SaveChanges();

            var signinCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: _authOptions.Issuer,
                 audience: _authOptions.Audience,
                 claims: new List<Claim>() { new Claim(ClaimTypes.Role, userRegisterDto.RoleFromRegister), new Claim(ClaimTypes.NameIdentifier, Profile.UserId.ToString()) },
                 expires: DateTime.Now.AddDays(30),
                 signingCredentials: signinCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);



            return  new OkObjectResult(new { AccessToken = encodedToken });

        }

        public async Task<IActionResult> GetToken(UserForLoginDto userLoginDto)
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
                return  new OkObjectResult(new { AccessToken = encodedToken });
            }
            return new UnauthorizedResult();
        }
    }
}
