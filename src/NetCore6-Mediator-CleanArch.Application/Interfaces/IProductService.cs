using NetCore6_Mediator_CleanArch.Application.DTOs;

namespace NetCore6_Mediator_CleanArch.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int? id);
    Task CreateProductyAsync(ProductDto product);
    Task UpdateProductyAsync(ProductDto product);
    Task DeleteProductyAsync(int? id);
}
