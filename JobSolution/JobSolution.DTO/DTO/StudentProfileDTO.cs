using JobSolution.Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.DTO.DTO
{
    public class StudentProfileDTO : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Univeristy { get; set; }
        public string ImagePath { get; set; }
    }
}
