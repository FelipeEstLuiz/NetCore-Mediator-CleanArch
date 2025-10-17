using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCore_Mediator_CleanArch.Application.DTOs;
using NetCore_Mediator_CleanArch.Application.Interfaces;

namespace NetCore_Mediator_CleanArch.WebUI.Controllers;

public class ProductsController(
    IProductService productAppService,
    ICategoryService categoryService,
    IWebHostEnvironment environment
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<ProductDto> products = await productAppService.GetProductsAsync();

        return View(products);
    }

    [HttpGet()]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await categoryService.GetCategoriesAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto request)
    {
        if (ModelState.IsValid)
        {
            await productAppService.CreateProductyAsync(request);
            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await productAppService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        IEnumerable<CategoryDto> categories = await categoryService.GetCategoriesAsync();

        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

        return View(productDto);
    }

    [HttpPost()]
    public async Task<IActionResult> Edit(ProductDto request)
    {
        if (ModelState.IsValid)
        {
            await productAppService.UpdateProductyAsync(request);
            return RedirectToAction(nameof(Index));
        }
        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await productAppService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        return View(productDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await productAppService.DeleteProductyAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await productAppService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        string wwwroot = environment.WebRootPath;
        string image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        bool exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDto);
    }
}
