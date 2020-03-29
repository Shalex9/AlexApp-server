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
            CreateMap<User, UserEF>()
                .ForMember(u => u.Id, opts => opts.Ignore());
            CreateMap<UserEF, User>();
        }
    }
}
