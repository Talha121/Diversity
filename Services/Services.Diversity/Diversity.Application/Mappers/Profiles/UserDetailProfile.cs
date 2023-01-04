﻿using AutoMapper;
using Diversity.Application.Models;
using Diversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Mappers.Profiles
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile()
        {
            CreateMap<UserDetail, UserDetailDTO>()
                .ForMember(s => s.UserAccount, x => x.MapFrom(t => t.UserAccounts != null ? t.UserAccounts : null));
            CreateMap<UserDetailDTO, UserDetail>();

            CreateMap<UserDetailDTO, UserLoginDTO>();
            CreateMap<UserLoginDTO, UserDetailDTO>();

        }
    }
}
