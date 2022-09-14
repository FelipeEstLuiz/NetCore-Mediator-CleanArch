using MediatR;
using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Application.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public int Id { get; private set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
