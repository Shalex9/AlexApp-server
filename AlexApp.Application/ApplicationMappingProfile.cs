using AutoMapper;
using AlexApp.Application.Dto;
using AlexApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<User, UserDto>();
        }

        //public static ReferenceDto EnumToReferenceDto<TEnum>(TEnum enumValue)
        //    where TEnum : Enum
        //{
        //    return new ReferenceDto((int)(object)enumValue, EnumHelper.GetEnumTitle(enumValue));
        //}
    }
}
