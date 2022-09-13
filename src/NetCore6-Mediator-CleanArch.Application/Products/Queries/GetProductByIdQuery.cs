using NetCore6_Mediator_CleanArch.Domain.Entities;
using MediatR;

namespace NetCore6_Mediator_CleanArch.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; private set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
