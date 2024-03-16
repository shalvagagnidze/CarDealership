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
        }
    }
}
