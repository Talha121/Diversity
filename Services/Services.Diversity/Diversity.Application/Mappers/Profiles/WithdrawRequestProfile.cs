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
    public class WithdrawRequestProfile : Profile
    {
        public WithdrawRequestProfile()
        {
            CreateMap<WithdrawRequest, WithdrawRequestDTO>()
                .ForMember(s => s.UserName, x => x.MapFrom(t => t.User.Name != null ? t.User.Name : null));
            CreateMap<WithdrawRequestDTO, WithdrawRequest>();

        }
    }
}
