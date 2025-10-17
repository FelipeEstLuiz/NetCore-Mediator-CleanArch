using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace NetCore_Mediator_CleanArch.Application.Products.Handlers;

public class ProductCreateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        Product product = new(request.Name, request.Description, request.Price, request.Stock, request.Image)
        {
            CategoryId = request.CategoryId
        };
        return await _productRepository.CreateAsync(product);
    }
}
