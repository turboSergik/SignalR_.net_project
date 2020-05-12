using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Domain.ConfigFluentAPI
{
    public class StudentJobConfig : IEntityTypeConfiguration<StudentJobConfig>
    {
        public void Configure(EntityTypeBuilder<StudentJobConfig> entity)
        {

        }
    }
}
