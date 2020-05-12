using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Infrastructure.Seed
{
    public class DbSeedUser
    {
        public static async Task PopulateUser(AppDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            string StudentRole = "Student";
            string EmployerRole = "Employer";

            var User_1 = new User()
            {
                UserName = "lascodaniil",
                Email = "lascodaniil@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };


            if (await userManager.FindByEmailAsync(User_1.Email) == null)
            {
                await userManager.CreateAsync(User_1, "password");
                await userManager.AddToRoleAsync(User_1, StudentRole);
                var User_1Str = new Profile()
                {
                    FirstName = "Daniil",
                    LastName = "Lasco",
                    Email = "lascodaniil@gmail.com",
                    DateOfBirth = new DateTime(1998, 10, 25),
                    University = "UTM",
                    UserId = 2,
                    PhoneNumber = "079854789"
                };
                dbContext.Profiles.Add(User_1Str);
            }


           // ++++++++++++++EMPLOYERS++++++++++++++++++++++++++++ +
           var User_3 = new User()
           {
               UserName = "PROIMOBIL",
               Email = "proimobil@proimobil.com",
               SecurityStamp = Guid.NewGuid().ToString()
           };

            if (await userManager.FindByEmailAsync(User_3.Email) == null)
            {
                await userManager.CreateAsync(User_3, "password");
                await userManager.AddToRoleAsync(User_3, EmployerRole);
                var User_1Str = new Profile()
                {
                    FirstName = "PROIMOBIL",
                    LastName = "SRL",
                    Email = "proimobil@proimobil.com",
                    University = "",
                    UserId = 3,
                    PhoneNumber = "022345678"
                };
                dbContext.Profiles.Add(User_1Str);
            }

            var User_4 = new User()
            {
                UserName = "AMDARISLLC",
                Email = "amdaris@amdaris.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (await userManager.FindByEmailAsync(User_4.Email) == null)
            {
                await userManager.CreateAsync(User_4, "password");
                await userManager.AddToRoleAsync(User_4, EmployerRole);
                var User_1Str = new Profile()
                {
                    FirstName = "AMDARIS",
                    LastName = "SRL",
                    Email = "amdaris@amdaris.com",
                    University = "",
                    UserId = 4,
                    PhoneNumber = "060982134"
                };
                dbContext.Profiles.Add(User_1Str);
            }



            var User_7 = new User()
            {
                UserName = "LINGUATA",
                Email = "linguata@linguata.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (await userManager.FindByEmailAsync(User_7.Email) == null)
            {
                await userManager.CreateAsync(User_7, "password");
                await userManager.AddToRoleAsync(User_7, EmployerRole);
                var User_1Str = new Profile()
                {
                    FirstName = "LINGUATA",
                    LastName = "SRL",
                    Email = "linguata@linguata.com",
                    University = "",
                    UserId = 5,
                    PhoneNumber = "06777534543"
                };
                dbContext.Profiles.Add(User_1Str);
            }

            var User_5 = new User()
            {
                UserName = "MERECEDES",
                Email = "mercedes@mercedes.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (await userManager.FindByEmailAsync(User_5.Email) == null)
            {
                await userManager.CreateAsync(User_5, "password");
                await userManager.AddToRoleAsync(User_5, EmployerRole);
                var User_1Str = new Profile()
                {
                    FirstName = "Mercedes",
                    LastName = "SRL",
                    Email = "mercedes@mercedes.com",
                    University = "",
                    UserId = 6,
                    PhoneNumber = "0223456078"
                };
                dbContext.Profiles.Add(User_1Str);
            }

        }


        public static async Task PopulateJobs(AppDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            var Job_1 = new Job()
            {
                CategoryId = 1,
                UserId = 4,
                Title = "Internship Programe",
                CityId = 1,
                PostDate = new DateTime(2020, 4, 1),
                EndDate = new DateTime(2020, 7, 1),
                ImagePath = "",
                Contact = "060277321",
                Salary = 0,
                TypeJobId = 1
            };
            dbContext.Jobs.Add(Job_1);

            var Job_2 = new Job()
            {
                CategoryId = 1,
                UserId = 4,
                Title = ".NET Developer",
                CityId = 2,
                PostDate = new DateTime(2020, 2, 1),
                EndDate = new DateTime(2020, 5, 31),
                ImagePath = "",
                Contact = "0602678972",
                Salary = 15,
                TypeJobId = 2
            };
            dbContext.Jobs.Add(Job_2);

            var Job_3 = new Job()
            {
                CategoryId = 4,
                UserId = 3,
                Title = "Sales Manager ",
                CityId = 3,
                PostDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 9, 11),
                ImagePath = "",
                Contact = "0603457665",
                Salary = 5,
                TypeJobId = 1
            };
            dbContext.Jobs.Add(Job_3);

            var Job_4 = new Job()
            {
                CategoryId = 3,
                UserId = 6,
                Title = "Service Auto",
                CityId = 3,
                PostDate = new DateTime(2020, 2, 14),
                EndDate = new DateTime(2020, 7, 11),
                ImagePath = "",
                Contact = "060345765",
                Salary = 10,
                TypeJobId = 3
            };
            dbContext.Jobs.Add(Job_4);


            var Job_5 = new Job()
            {
                CategoryId = 2,
                UserId = 5,
                Title = "English Teacher",
                CityId = 2,
                PostDate = new DateTime(2020, 2, 14),
                EndDate = new DateTime(2020, 7, 11),
                ImagePath = "",
                Contact = "060353453765",
                Salary = 5,
                TypeJobId = 2
            };
            dbContext.Jobs.Add(Job_5);
        }

    }
}
