using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.ViewModels.Product;

namespace Web.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Product(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product is null) 
				return NotFound();

			return View(_mapper.Map<Product, ProductFullViewModel>(product));
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<ProductShortViewModel>> GetAllProducts([FromQuery] ProductSpecParam specParam)
		{
			var products = await _productService.GetAllProductsAsync(specParam);

			return _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductShortViewModel>>(products);
		}

		[HttpGet("add")]
		public IActionResult AddProduct()
		{
			return View();
		}

		[HttpPost("add")]
		[InvalidModelFilter]
		public async Task<IActionResult> AddProduct(ProductAddViewModel productToAdd)
		{
			var product = _mapper.Map<ProductAddViewModel, Product>(productToAdd);
			await _productService.AddProductAsync(product);

			return RedirectToAction("Index");
		}

		[HttpGet("edit/{id}")]
		public async Task<IActionResult> EditProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product is null) 
				return NotFound();

			return View(_mapper.Map<Product, ProductEditViewModel>(product));
		}

		[HttpPost("edit/{id}")]
		[InvalidModelFilter]
		public async Task<IActionResult> EditProduct(ProductEditViewModel productToEdit)
		{
			var product = _mapper.Map<ProductEditViewModel, Product>(productToEdit);
			await _productService.UpdateProductAsync(product);

			return RedirectToAction("Index");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				await _productService.DeleteProductAsync(id);
			}
			catch (NullReferenceException)
			{
				return NotFound();
			}
			return Ok();
		}
	}
}
