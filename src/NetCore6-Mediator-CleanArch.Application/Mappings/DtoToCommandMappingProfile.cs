using AutoMapper;
using NetCore6_Mediator_CleanArch.Application.Categories.Commands;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Products.Commands;

namespace NetCore6_Mediator_CleanArch.Application.Mappings
{
    public class DtoToCommandMappingProfile : Profile
    {
        public DtoToCommandMappingProfile()
        {
            CreateMap<ProductDto, ProductCreateCommand>();
            CreateMap<ProductDto, ProductUpdateCommand>();

            CreateMap<CategoryDto, CategoryCreateCommand>();
            CreateMap<CategoryDto, CategoryUpdateCommand>();
        }
    }
}
