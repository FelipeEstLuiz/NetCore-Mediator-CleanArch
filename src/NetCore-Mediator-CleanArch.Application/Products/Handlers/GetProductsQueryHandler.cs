using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Queries;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Products.Handlers;

public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) 
        => await _productRepository.GetProductsAsync();
}
