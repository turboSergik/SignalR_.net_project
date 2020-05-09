using JobSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Domain.ConfigFluentAPI
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> entity)
        {
            
            entity.Property(p => p.Title).HasMaxLength(255).IsRequired();
            entity.Property(p => p.Contact).HasMaxLength(255).IsRequired();
            
        }
    }
}
