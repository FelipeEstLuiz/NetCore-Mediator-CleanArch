using MediatR;
using NetCore_Mediator_CleanArch.Application.Categories.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryCreateCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryCreateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));

    public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.Name);
        return await _categoryRepository.CreateAsync(category);

    }
}
