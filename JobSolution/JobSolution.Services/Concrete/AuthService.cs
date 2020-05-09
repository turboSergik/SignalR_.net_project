using AutoMapper;
using JobSolution.Domain;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Configuration;
using JobSolution.Infrastructure.Database;
using JobSolution.Repository.Interfaces;
using JobSolution.Services.Interfaces;
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

namespace JobSolution.Services.Concrete
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
            UserManager<User> userManager,IHttpContextAccessor context, IHostingEnvironment hostingEnvironment)
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
            UserRegisterDto registerUser = null;
            
            try
            {
                foreach (var key in _context.HttpContext.Request.Form.Keys)
                {
                    registerUser = JsonConvert.DeserializeObject<UserRegisterDto>(_context.HttpContext.Request.Form[key]);
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
                            registerUser.ImagePath = fullPath;
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


            if(registerUser == null)
            {

                return new StatusCodeResult(500);
            }

            var user = await _userManager.FindByNameAsync(registerUser.UserName);
            if(user != null)
            {

                return new BadRequestObjectResult ("Username or email exists");
            }


            var AddUser = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName,
                Email = registerUser.Email
            };

            await _userManager.CreateAsync(AddUser, registerUser.Password);
            await _userManager.AddToRoleAsync(AddUser, registerUser.RoleFromRegister);

            _dbContext.Profiles.Add(_mapper.Map<JobSolution.Domain.Entities.Profile>(registerUser));
            _dbContext.SaveChanges();


            var signInCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: _authOptions.Issuer,
                 audience: _authOptions.Audience,
                 claims: new List<Claim>() { new Claim(ClaimTypes.Role, registerUser.RoleFromRegister), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) },
                 expires: DateTime.Now.AddDays(30),
                 signingCredentials: signInCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

            return new OkObjectResult(new { AccessToken = encodedToken });

        }

        public Task<IActionResult> GetToken()
        {
            throw new NotImplementedException();
        }
    }
}
