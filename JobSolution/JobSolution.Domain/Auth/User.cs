using JobSolution.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JobSolution.Domain.Auth
{
    public class User : IdentityUser<int>
    {
        public virtual Profile Profile{ get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<StudentJobs> StudentJobs { get; set; }
    }
}
