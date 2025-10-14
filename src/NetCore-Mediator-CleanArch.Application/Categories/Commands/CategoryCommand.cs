using MediatR;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Categories.Commands;

public abstract class CategoryCommand : IRequest<Category>
{
    public string? Name { get; set; }
}
