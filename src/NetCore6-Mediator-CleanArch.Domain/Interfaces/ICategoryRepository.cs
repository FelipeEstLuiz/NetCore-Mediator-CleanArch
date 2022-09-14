using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int? id);
    Task<bool> CategoryProductAsync(int? id);
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<Category> RemoveAsync(Category category);
}
