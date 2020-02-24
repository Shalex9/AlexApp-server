using AutoMapper;
using AlexApp.Data.Models;
using AlexApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<User, UserEF>();
            CreateMap<UserEF, User>();
        }
    }
}
