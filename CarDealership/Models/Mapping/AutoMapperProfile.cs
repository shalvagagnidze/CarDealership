using AutoMapper;
using CarDealership.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarDealership.Models.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<CarCategory, CarCategoryModel>();
            CreateMap<CarCategoryModel, CarCategory>();
            CreateMap<CarBrand, CarBrandModel>();
            CreateMap<CarBrandModel, CarBrand>();
            CreateMap<CarModel,CarModelDto>();
            CreateMap<CarModelDto, CarModel>();
        }
    }
}
