using AutoMapper;
using GroceryDistibution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryDistibution.BLL.ViewModels
{
    class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ApplicationReleaseVM, ApplicationRelease>();
            CreateMap<ApplicationRelease, ApplicationReleaseVM>();
            CreateMap<UserTypeVM, UserType>().ReverseMap();         // ReverseMap() = does mapping S -> D & vice versa
            //CreateMap<UserType, UserTypeVM>();
            CreateMap<CountryVM, Country>().ReverseMap();
            //CreateMap<Country, CountryVM>();
            CreateMap<StateVM, State>().ReverseMap();
            //CreateMap<State, StateVM>();
            CreateMap<CityVM, City>().ReverseMap();
            //CreateMap<City, CityVM>();
            CreateMap<UserVM, User>().ReverseMap();
            //CreateMap<User, UserVM>();
        }
    }
}
