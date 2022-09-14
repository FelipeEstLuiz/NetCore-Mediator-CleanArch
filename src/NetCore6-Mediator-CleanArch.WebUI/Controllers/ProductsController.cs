using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCore6_Mediator_CleanArch.Application.DTOs;
using NetCore6_Mediator_CleanArch.Application.Interfaces;

namespace NetCore6_Mediator_CleanArch.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(
        IProductService productAppService,
        ICategoryService categoryService,
        IWebHostEnvironment environment
    )
    {
        _productService = productAppService;
        _categoryService = categoryService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<ProductDto> products = await _productService.GetProductsAsync();

        return View(products);
    }

    [HttpGet()]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto request)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductyAsync(request);
            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await _productService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        IEnumerable<CategoryDto> categories = await _categoryService.GetCategoriesAsync();

        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

        return View(productDto);
    }

    [HttpPost()]
    public async Task<IActionResult> Edit(ProductDto request)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProductyAsync(request);
            return RedirectToAction(nameof(Index));
        }
        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await _productService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        return View(productDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductyAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        ProductDto productDto = await _productService.GetProductByIdAsync(id);

        if (productDto == null) return NotFound();

        string wwwroot = _environment.WebRootPath;
        string image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        bool exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDto);
    }
}
