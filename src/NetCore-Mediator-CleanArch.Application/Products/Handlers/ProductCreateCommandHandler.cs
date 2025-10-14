using MediatR;
using NetCore_Mediator_CleanArch.Application.Products.Commands;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Products.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public ProductCreateCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        Product product = new(request.Name, request.Description, request.Price, request.Stock, request.Image);

        if (product == null)
            throw new ApplicationException("Error creating entity.");

        product.CategoryId = request.CategoryId;
        return await _productRepository.CreateAsync(product);
    }
}
