using JobSolution.Domain.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobSolution.Domain.Entities
{
    public class Profile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegisterDate { get; set; } = DateTime.Now;

    }      
}
