using JobSolution.Domain.Entities;
using System;

namespace JobSolution.DTO.DTO
{
    public class ProfileDTO : BaseEntity
    {
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string University { get; set; }
        public DateTime ? DateOfBirth { get; set; }

    }
}
