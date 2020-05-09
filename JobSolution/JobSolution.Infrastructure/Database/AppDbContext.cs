using JobSolution.Domain;
using JobSolution.Domain.Auth;
using JobSolution.Domain.ConfigFluentAPI;
using JobSolution.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSolution.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)  :base(dbContextOptions){ }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<TypeJob> TypeJobs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProfileConfig());
            modelBuilder.ApplyConfiguration(new JobConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            ApplyIdentityMapConfiguration(modelBuilder);
        }

        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", SchemaConsts.Auth);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConsts.Auth);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConsts.Auth);
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConsts.Auth);
            modelBuilder.Entity<Role>().ToTable("Roles", SchemaConsts.Auth);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConsts.Auth);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConsts.Auth);
        }
    }
}
