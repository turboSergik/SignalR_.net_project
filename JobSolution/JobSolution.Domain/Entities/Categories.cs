using System.Collections.Generic;

namespace JobSolution.Domain.Entities
{
    public class Categories : BaseEntity
    {
        public string Category { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }


}
