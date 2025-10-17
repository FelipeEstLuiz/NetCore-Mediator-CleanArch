using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Products.Handlers;

public class ProductRemoveCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.GetProductById(request.Id) ?? throw new InvalidOperationException("Entity could not be found");
        Product result = await _productRepository.RemoveAsync(product);
        return result;
    }
}
