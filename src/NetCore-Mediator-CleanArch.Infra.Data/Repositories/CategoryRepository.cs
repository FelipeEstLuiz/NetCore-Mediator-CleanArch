using Microsoft.EntityFrameworkCore;
using NetCore_Mediator_CleanArch.Domain.Entities;
using NetCore_Mediator_CleanArch.Domain.Interfaces;
using NetCore_Mediator_CleanArch.Infra.Data.Context;

namespace NetCore_Mediator_CleanArch.Infra.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<bool> CategoryProductAsync(int? id) => await context.Products.FirstOrDefaultAsync(x => x.CategoryId == id) is not null;

    public async Task<Category> CreateAsync(Category category)
    {
        context.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await context.Categories.ToListAsync();

    public async Task<Category?> GetCategoryByIdAsync(int? id) => await context.Categories.FindAsync(id);

    public async Task<Category> RemoveAsync(Category category)
    {
        context.Remove(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        context.Update(category);
        await context.SaveChangesAsync();
        return category;
    }
}
