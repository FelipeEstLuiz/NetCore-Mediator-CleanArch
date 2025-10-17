using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;

namespace NetCore_Mediator_CleanArch.WebUI.Controllers;

[Authorize]
public class CategoriesController(ICategoryService categoryService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<CategoryDto> categories = await categoryService.GetCategoriesAsync();

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            await categoryService.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        CategoryDto categoryDto = await categoryService.GetCategoryByIdAsync(id);

        if (categoryDto == null) return NotFound();

        return View(categoryDto);
    }

    [HttpPost()]
    public async Task<IActionResult> Edit(CategoryDto categoryDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await categoryService.UpdateCategoryAsync(categoryDto);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoryDto);
    }

    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        CategoryDto categoryDto = await categoryService.GetCategoryByIdAsync(id);

        if (categoryDto == null) return NotFound();

        return View(categoryDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        CategoryDto categoryDto = await categoryService.GetCategoryByIdAsync(id);

        if (categoryDto == null) return NotFound();

        return View(categoryDto);
    }
}
