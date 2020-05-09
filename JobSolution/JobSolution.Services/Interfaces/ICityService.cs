using JobSolution.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Services.Interfaces
{
   public interface ICityService
    {
        Task<IList<CityDTO>> GetCities();
    }
}
