using AutoMapper;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
