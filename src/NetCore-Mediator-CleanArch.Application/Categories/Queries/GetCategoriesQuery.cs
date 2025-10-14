using MediatR;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Categories.Queries;

public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
{
}
