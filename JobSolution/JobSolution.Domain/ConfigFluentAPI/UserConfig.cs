using JobSolution.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSolution.Domain.ConfigFluentAPI
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasMany(x => x.Jobs).WithOne(y => y.User).HasForeignKey(x => x.UserId);
         

        }
    }
}
