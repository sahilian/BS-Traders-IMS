using AutoMapper;
using BSIMS.Application.DTOs;
using BSIMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSIMS.Application.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
           // CreateMap<ProductDto, Product>()
           // .ForMember(dest => dest.SupplierProducts, opt => opt.MapFrom(src => src.SupplierProducts));

           // CreateMap<Product, ProductDto>()
           //.ForMember(dest => dest.SupplierProducts, opt => opt.MapFrom(src => src.SupplierProducts));

            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<SupplierDto, Supplier>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<SupplierProductDto, SupplierProduct>().ReverseMap();
        }
    }
}
