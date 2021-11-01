using AutoMapper;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, MyDemenageurUser>();
                //.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.RegisterUsername));
            CreateMap<UserUpdateModel, MyDemenageurUser>();
        }
    }
}
