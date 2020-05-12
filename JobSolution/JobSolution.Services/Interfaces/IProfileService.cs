using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetAuthProfile();
        

    }
}