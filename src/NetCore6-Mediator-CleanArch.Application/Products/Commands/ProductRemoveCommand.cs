using NetCore6_Mediator_CleanArch.Domain.Entities;
using MediatR;

namespace NetCore6_Mediator_CleanArch.Application.Products.Commands
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public int Id { get; private set; }

        public ProductRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
