using JobSolution.Domain;
using JobSolution.Domain.Auth;
using JobSolution.Domain.Entities;
using JobSolution.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.API.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {   
        public AutoMapperProfile()
        {
            CreateMap<Job, JobDTO>().ForMember(x => x.PublishedOn, y => y.MapFrom(z => z.PostDate))
                                    .ForMember(x => x.FinishedOn, y => y.MapFrom(z => z.EndDate))
                                    .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.Category))
                                    .ForMember(x => x.JobId, y => y.MapFrom(z => z.Id))
                                    .ForMember(x => x.Employer, y => y.MapFrom(x => x.User.Profile.FirstName + " " + x.User.Profile.LastName))
                                    .ForMember(x => x.City, y => y.MapFrom(x => x.Cities.City))
                                    .ForMember(x=>x.TypeJob , y=>y.MapFrom(x=>x.TypeJob.Name))
                                    .ForMember(x=>x.Marked , y=>y.MapFrom(x=>x.Marked));
                                    
            
            CreateMap<JobDTO, Job>().ForMember(x => x.PostDate, y => y.MapFrom(z => z.PublishedOn))
                                    .ForMember(x => x.EndDate, y => y.MapFrom(z => z.FinishedOn))
                                    .ForMember(x => x.Category, y => y.Ignore());

            CreateMap<Advert, AdvertDTO>().ForMember(x => x.AdvertId, y => y.MapFrom(y => y.Id))
                                          .ForMember(x => x.City, y => y.MapFrom(z => z.Cities.City))
                                          .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.Category))
                                          .ForMember(x => x.City, y => y.MapFrom(z => z.Cities.City))
                                          .ForMember(x => x.PublishedOn, y => y.MapFrom(z => z.PostDate));
            
            CreateMap<AdvertDTO, Advert>().ForMember(x => x.Id, y => y.MapFrom(x => x.AdvertId))
                .ForMember(x => x.Category, y => y.Ignore());

            CreateMap<TypeJob, TypeJobDTO>();


            CreateMap<JobSolution.Domain.Entities.Profile, StudentProfileDTO>()
                .ForMember(x => x.ImagePath, y => y.MapFrom(z => z.ImagePath))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName));

            CreateMap<JobSolution.Domain.Entities.Profile, EmployerProfileDTO>()
                .ForMember(x => x.Base64Photo, y => y.MapFrom(z => z.ImagePath))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.PhoneNumber))
                .ForMember(x => x.RegistrationDate, y => y.MapFrom(x => x.RegisterDate))
                .ForMember(x => x.FullName, y => y.MapFrom(x => x.FirstName + " " + x.LastName));


            CreateMap<UserRegisterDto, JobSolution.Domain.Entities.Profile>()
                .ForMember(x => x.DateOfBirth, x => x.MapFrom(y => y.DateOfBirth))
                .ForMember(x => x.Email, x => x.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, y => y.MapFrom(y => y.FirstName))
                .ForMember(x => x.ImagePath, y => y.MapFrom(y => y.ImagePath))
                .ForMember(x => x.LastName, y => y.MapFrom(y => y.LastName))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(y => y.PhoneNumber));
                

            CreateMap<Categories, CategoryDTO>()
                .ForMember(x => x.Category, y => y.MapFrom(x => x.Category));
            CreateMap<Cities, CityDTO>()
                .ForMember(x => x.City, y => y.MapFrom(x => x.City));
        }
    }
}
