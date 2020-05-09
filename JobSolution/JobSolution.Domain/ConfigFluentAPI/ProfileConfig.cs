using JobSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Domain.ConfigFluentAPI
{
    public class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> entity)
        {
            entity.HasIndex(p => p.Email).IsUnique();
            entity.Property(p => p.Email).HasMaxLength(255).IsRequired();
            entity.Property(p => p.PhoneNumber).IsRequired();
            entity.HasIndex(p => p.PhoneNumber).IsUnique();
        }
    }
}
