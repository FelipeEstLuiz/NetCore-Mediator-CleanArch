using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Products.Handlers;

public class ProductUpdateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.GetProductById(request.Id) ?? throw new InvalidOperationException("Entity could not be found");

        product.Update(
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.Image,
            request.CategoryId
        );

        return await _productRepository.UpdateAsync(product);
    }
}
