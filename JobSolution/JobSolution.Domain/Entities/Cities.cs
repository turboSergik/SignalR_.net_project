using System.Collections.Generic;

namespace JobSolution.Domain.Entities
{
    public class Cities : BaseEntity
    {
        public string City { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
