using MediatR;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;
using NetCore_Mediator_CleanArch.Application.Products.Commands;
using NetCore_Mediator_CleanArch.Application.Products.Queries;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Application.Services;

public class ProductService(IMediator mediator) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        GetProductsQuery productsQuery = new();

        IEnumerable<Product> result = await mediator.Send(productsQuery);

        return result.Select(x => new ProductDto()
        {
            Id = x.Id,
            Name = x.Name,
            Category = x.Category,
            CategoryId = x.CategoryId ?? 0,
            Description = x.Description,
            Price = x.Price,
            Stock = x.Stock,
            Image = x.Image
        });
    }

    public async Task<ProductDto> GetProductByIdAsync(int? id)
    {
        GetProductByIdQuery productByIdQuery = new(id ?? throw new InvalidOperationException("Product is required."));

        Product result = await mediator.Send(productByIdQuery) ?? throw new InvalidOperationException("Product not found.");

        return new ProductDto()
        {
            Id = result.Id,
            Name = result.Name,
            Category = result.Category,
            CategoryId = result.CategoryId ?? 0,
            Description = result.Description,
            Price = result.Price,
            Stock = result.Stock,
            Image = result.Image
        };
    }

    public async Task CreateProductyAsync(ProductDto product)
    {
        ProductCreateCommand productCreateCommand = new()
        {
            Image = product.Image,
            Stock = product.Stock,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Name = product.Name
        };
        await mediator.Send(productCreateCommand);
    }

    public async Task UpdateProductyAsync(ProductDto product)
    {
        ProductUpdateCommand productUpdateCommand = new()
        {
            Image = product.Image,
            Stock = product.Stock,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Id = product.Id
        };
        await mediator.Send(productUpdateCommand);
    }

    public async Task DeleteProductyAsync(int? id)
    {
        ProductRemoveCommand productRemoveCommand = new(id ?? throw new InvalidOperationException("Product is required."));

        await mediator.Send(productRemoveCommand);
    }
}
