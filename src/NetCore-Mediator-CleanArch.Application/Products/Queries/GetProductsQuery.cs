using MediatR;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}
