using NetCore_Mediator_CleanArch.Domain.Entities;
using MediatR;

namespace NetCore_Mediator_CleanArch.Application.Products.Queries;

public class GetProductByIdQuery(int id) : IRequest<Product?>
{
    public int Id { get; private set; } = id;
}
