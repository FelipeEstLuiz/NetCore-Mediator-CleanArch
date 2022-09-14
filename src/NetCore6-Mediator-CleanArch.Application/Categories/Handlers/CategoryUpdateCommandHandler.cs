using MediatR;
using NetCore6_Mediator_CleanArch.Application.Categories.Commands;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore6_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryUpdateCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
    }

    public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

        if (category == null)
            throw new ApplicationException("Entity could not be found");

        category.Update(request.Name);

        return await _categoryRepository.UpdateAsync(category);
    }
}
