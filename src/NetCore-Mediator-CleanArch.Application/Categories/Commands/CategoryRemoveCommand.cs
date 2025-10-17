using MediatR;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Categories.Commands;

public class CategoryRemoveCommand(int id) : IRequest<Category>
{
    public int Id { get; private set; } = id;
}
