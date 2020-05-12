using JobSolution.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Domain.Entities
{
    public class StudentJobs : BaseEntity
    {

        public int? UserId { get; set; }
        public User User { get; set;  }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
