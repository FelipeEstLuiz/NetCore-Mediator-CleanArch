using MediatR;
using NetCore_Mediator_CleanArch.Application.Categories.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryRemoveCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryRemoveCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));

    public async Task<Category> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository.GetCategoryByIdAsync(request.Id)
            ?? throw new InvalidOperationException("Entity could not be found");

        bool exists = await _categoryRepository.CategoryProductAsync(category.Id);

        if (exists)
            throw new InvalidOperationException("Action not allowed, category linked to a product");

        Category result = await _categoryRepository.RemoveAsync(category);
        return result;
    }
}
