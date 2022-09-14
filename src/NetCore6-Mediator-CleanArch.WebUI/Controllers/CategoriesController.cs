using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Interfaces;

namespace NetCore6_Mediator_CleanArch.WebUI.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<CategoryDto> categories = await _categoryService.GetCategoriesAsync();

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
            await _categoryService.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(id);

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
                await _categoryService.UpdateCategoryAsync(categoryDto);
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

        CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(id);

        if (categoryDto == null) return NotFound();

        return View(categoryDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(id);

        if (categoryDto == null) return NotFound();

        return View(categoryDto);
    }
}
