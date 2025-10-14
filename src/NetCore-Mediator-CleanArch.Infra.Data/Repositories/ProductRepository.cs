using Microsoft.EntityFrameworkCore;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;
using NetCore_Mediator_CleanArch.Infra.Data.Context;

namespace NetCore_Mediator_CleanArch.Infra.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        context.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetProductById(int? id) => await context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetProductsAsync() => await context.Products.ToListAsync();

    public async Task<Product> RemoveAsync(Product product)
    {
        context.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        context.Update(product);
        await context.SaveChangesAsync();
        return product;
    }
}
