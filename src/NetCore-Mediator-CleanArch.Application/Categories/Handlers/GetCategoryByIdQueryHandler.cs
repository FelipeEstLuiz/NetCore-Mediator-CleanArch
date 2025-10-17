using MediatR;
using NetCore_Mediator_CleanArch.Application.Categories.Queries;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Categories.Handlers;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentException(nameof(categoryRepository));

    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) => await _categoryRepository.GetCategoryByIdAsync(request.Id);
}
