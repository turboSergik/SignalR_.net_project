using JobSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.DTO.DTO
{
   public class StudentJobDTO 
   {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public DateTime? PostDate { get; set; }
        public string AuthorName{ get; set; } 
        public string CategoryName { get; set; }
    }
}

