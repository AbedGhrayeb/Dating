using AutoMapper;
using Dating.Helpers;
using Dating.Models;
using Dating.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UsersListViewModel>()
                .ForMember(dest => dest.PhotoUrl, options =>
                {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, options =>
                 {
                     options.MapFrom(d => d.DateOfBirth.CalculatAge());
                 });

            CreateMap<AppUser, UserDetailesViewModel>()
                .ForMember(dest => dest.PhotoUrl, options =>
                 {
                     options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                 })
                .ForMember(dest => dest.Age, options =>
                {
                    options.MapFrom(d => d.DateOfBirth.CalculatAge());
                }); ;

            CreateMap<UserDetailesViewModel, AppUser>();
            CreateMap<Photo, PhotosListViewModel>();
            CreateMap<PhotosListViewModel,Photo>();
        }
    }
}
