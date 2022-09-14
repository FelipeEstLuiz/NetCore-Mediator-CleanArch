using MediatR;
using NetCore6_Mediator_CleanArch.Application.Categories.Commands;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore6_Mediator_CleanArch.Application.Categories.Handlers;

public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryCreateCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));
    }

    public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.Name);

        if (category == null)
            throw new ApplicationException("Error creating entity.");

        return await _categoryRepository.CreateAsync(category);

    }
}
