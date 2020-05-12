using JobSolution.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IActionResult> AddUser();
        Task<IActionResult> GetToken(UserForLoginDto userLoginDto);
    }
}
