using AutoMapper;
using Diversity.Application.Models;
using Diversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Mappers.Profiles
{
    internal class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountDTO>()
                .ForMember(s => s.UserName, x => x.MapFrom(t => t.User.Name != null ? t.User.Name : null));
            CreateMap<UserAccountDTO, UserAccount>();

        }
    }
}
