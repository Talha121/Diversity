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
                .ForMember(s => s.productImages, x => x.MapFrom(t => t.ProductImages != null ? t.ProductImages : null));
            CreateMap<GetProductDTO, Product>()
               .ForMember(s => s.ProductImages, x => x.MapFrom(t => t.productImages != null ? t.productImages : null));

            CreateMap<ProductImage, ProductImageDTO>();
            CreateMap<ProductImageDTO, ProductImage>();



        }
    }
}
