using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Infrastructure.Database
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            CategorySeeder.CreateCategories(dbContext);
            CategorySeeder.CreateCities(dbContext);
            CategorySeeder.CategoryJob(dbContext);
            CreateUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            DbSeedUser.PopulateUser(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            DbSeedUser.PopulateJobs(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            dbContext.SaveChanges();
        }

        private static async Task CreateUsers(AppDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            string AdministratorRole = "Administrator";
            string StudentRole = "Student";
            string EmployerRole = "Employer";

            if (!await roleManager.RoleExistsAsync(AdministratorRole))
            {
                await roleManager.CreateAsync(new Role(AdministratorRole));
            }

            if (!await roleManager.RoleExistsAsync(StudentRole))
            {
                await roleManager.CreateAsync(new Role(StudentRole));
            }
            if (!await roleManager.RoleExistsAsync(EmployerRole))
            {
                await roleManager.CreateAsync(new Role(EmployerRole));
            }

            var user_Admin = new User()
            {
                UserName = "Admin",
                Email = "admin@admin.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var UserProfile = new Profile()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin.com",
                RegisterDate = DateTime.Now,
                University = "",
                PhoneNumber= "0000000000",
                UserId = 1
            };


            if (await userManager.FindByEmailAsync(user_Admin.Email) == null)
            {
                await userManager.CreateAsync(user_Admin, "password");
                await userManager.AddToRoleAsync(user_Admin, AdministratorRole);
                await userManager.AddToRoleAsync(user_Admin, StudentRole);
                await userManager.AddToRoleAsync(user_Admin, EmployerRole);
            
                dbContext.Profiles.Add(UserProfile);
            
            }
        }
    }
}
