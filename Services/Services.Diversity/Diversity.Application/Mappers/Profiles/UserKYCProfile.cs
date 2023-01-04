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
    public class UserKYCProfile: Profile
    {
        public UserKYCProfile()
        {
            CreateMap<UserKYC, UserKYCDTO>();
            CreateMap<UserKYCDTO, UserKYC>();
        }
    }
}
