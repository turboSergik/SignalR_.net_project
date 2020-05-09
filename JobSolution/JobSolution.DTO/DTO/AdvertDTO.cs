using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.DTO.DTO
{
    public class AdvertDTO
    {
        public int AdvertId { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }

    }
}
