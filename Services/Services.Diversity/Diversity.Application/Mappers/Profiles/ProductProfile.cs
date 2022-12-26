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
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, GetProductDTO>()
                .ForMember(s => s.productImages, x => x.MapFrom(t => t.ProductImages != null ? t.ProductImages : null)); ;
        }
    }
}
