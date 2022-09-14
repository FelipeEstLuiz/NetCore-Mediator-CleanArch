using AutoMapper;
using MediatR;
using NetCore6_Mediator_CleanArch.Application.Categories.Commands;
using NetCore6_Mediator_CleanArch.Application.Categories.Queries;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Interfaces;
using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        GetCategoriesQuery getCategoriesQuery = new();

        if (getCategoriesQuery == null)
            throw new Exception("Entity could not be load.");

        IEnumerable<Category> categories = await _mediator.Send(getCategoriesQuery);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int? id)
    {
        GetCategoryByIdQuery getCategoryByIdQuery = new(id ?? throw new Exception("Category is required."));

        if (getCategoryByIdQuery == null)
            throw new Exception("Entity could not be load.");

        Category category = await _mediator.Send(getCategoryByIdQuery);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task CreateCategoryAsync(CategoryDto category)
    {
        CategoryCreateCommand categoryCreateCommand = _mapper.Map<CategoryCreateCommand>(category);
        await _mediator.Send(categoryCreateCommand);
    }

    public async Task UpdateCategoryAsync(CategoryDto category)
    {
        CategoryUpdateCommand categoryUpdateCommand = _mapper.Map<CategoryUpdateCommand>(category);
        await _mediator.Send(categoryUpdateCommand);
    }

    public async Task DeleteCategoryAsync(int? id)
    {
        CategoryRemoveCommand categoryRemoveCommand = new(id ?? throw new Exception("Category is required."));

        if (categoryRemoveCommand == null)
            throw new Exception("Entity could not be load.");

        await _mediator.Send(categoryRemoveCommand);
    }
}
