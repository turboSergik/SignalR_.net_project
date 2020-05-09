
using JobSolution.Domain.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSolution.Domain.Entities
{
    public class Job : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Contact { get; set; } 
        public string Description { get; set; }
        public DateTime? PostDate { get; set; } = DateTime.Now; 
        public DateTime? EndDate { get; set; } 
        public DateTime? EnrolledDate { get; set; }
        public string ImagePath { get; set; } 
        public float Salary { get; set; }
        [ForeignKey("Cities")]
        public int CityId { get; set; }
        public Cities Cities { get; set; }
        public Categories Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("TypeJob")]
        public int TypeJobId { get; set; }
        public TypeJob TypeJob { get; set; }
        public bool Marked { get; set; }

    }
}
