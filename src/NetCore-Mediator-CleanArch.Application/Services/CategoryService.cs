using MediatR;
using NetCore_Mediator_CleanArch.Application.Categories.Commands;
using NetCore_Mediator_CleanArch.Application.Categories.Queries;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Services;

public class CategoryService(IMediator mediator) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        GetCategoriesQuery getCategoriesQuery = new();

        IEnumerable<Category> categories = await mediator.Send(getCategoriesQuery);
        return categories.Select(x => new CategoryDto()
        {
            Id = x.Id,
            Name = x.Name
        });
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int? id)
    {
        GetCategoryByIdQuery getCategoryByIdQuery = new(id ?? throw new InvalidOperationException("Category is required."));

        Category category = await mediator.Send(getCategoryByIdQuery);
        return new CategoryDto()
        {
            Name = category.Name,
            Id = category.Id
        };
    }

    public async Task CreateCategoryAsync(CategoryDto category)
    {
        CategoryCreateCommand categoryCreateCommand = new()
        {
            Name = category.Name
        };
        await mediator.Send(categoryCreateCommand);
    }

    public async Task UpdateCategoryAsync(CategoryDto category)
    {
        CategoryUpdateCommand categoryUpdateCommand = new()
        {
            Name = category.Name,
            Id = category.Id
        };
        await mediator.Send(categoryUpdateCommand);
    }

    public async Task DeleteCategoryAsync(int? id)
    {
        CategoryRemoveCommand categoryRemoveCommand = new(id ?? throw new InvalidOperationException("Category is required."));
        await mediator.Send(categoryRemoveCommand);
    }
}
