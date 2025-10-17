using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Queries;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Products.Handlers;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) 
        => await _productRepository.GetProductById(request.Id);
}
