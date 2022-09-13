using MediatR;
using NetCore6_Mediator_CleanArch.Application.Products.Commands;
using NetCore6_Mediator_CleanArch.Domain.Entities;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetProductById(request.Id);

            if (product == null)
                throw new ApplicationException("Entity could not be found");

            Product result = await _productRepository.RemoveAsync(product);
            return result;
        }
    }
}
