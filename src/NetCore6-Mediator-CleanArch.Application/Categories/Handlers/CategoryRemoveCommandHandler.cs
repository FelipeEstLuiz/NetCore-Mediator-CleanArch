using MediatR;
using NetCore6_Mediator_CleanArch.Application.Categories.Commands;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore6_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRemoveCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
    }

    public async Task<Category> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

        if (category == null)
            throw new ApplicationException("Entity could not be found");

        bool exists = await _categoryRepository.CategoryProductAsync(category.Id);

        if (exists)
            throw new ApplicationException("Action not allowed, category linked to a product");

        Category result = await _categoryRepository.RemoveAsync(category);
        return result;
    }
}
