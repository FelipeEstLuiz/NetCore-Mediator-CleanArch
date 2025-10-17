using MediatR;
using NetCore_Mediator_CleanArch.Application.Categories.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryUpdateCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryUpdateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));

    public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository.GetCategoryByIdAsync(request.Id)
            ?? throw new InvalidOperationException("Entity could not be found");

        category.Update(request.Name);

        return await _categoryRepository.UpdateAsync(category);
    }
}
