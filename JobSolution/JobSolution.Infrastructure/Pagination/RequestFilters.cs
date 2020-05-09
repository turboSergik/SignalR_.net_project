using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Infrastructure.Pagination
{
    public class RequestFilters
    {
        public RequestFilters()
        {
            Filters = new List<Filter>();
        }

        public FilterLogicalOperators LogicalOperator { get; set; }

        public IList<Filter> Filters { get; set; }
    }




}
