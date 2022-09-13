using MediatR;
using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Application.Categories.Commands
{
    public abstract class CategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
