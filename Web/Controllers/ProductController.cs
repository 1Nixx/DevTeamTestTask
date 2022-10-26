using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Product;

namespace Web.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("[controller]")]
	public class ProductController : Controller
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IMapper _mapper;

		public ProductController(IGenericRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;	
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
			var spec = new ProductFullInfo(id);

			var product = await _productRepository.GetEntityWithSpec(spec);

			if (product is null) return NotFound();
			
			return View(_mapper.Map<Product, ProductFullViewModel>(product));
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<ProductShortViewModel>> GetAllProducts([FromQuery]ProductSpecParam specParam)
		{
			var spec = new ProductsFilteredByShop(specParam);

			var products = await _productRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductShortViewModel>>(products);		
		}

		[HttpGet("add")]
		public IActionResult AddProduct()
		{
			return View();
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddProduct(ProductAddViewModel productToAdd)
		{
			if (!ModelState.IsValid)
				return View(productToAdd);

			var product = _mapper.Map<ProductAddViewModel, Product>(productToAdd);

			await _productRepository.AddAsync(product);
			await _productRepository.SaveAsync();

			return RedirectToAction("Index");
		}

		[HttpGet("edit/{id}")]
		public async Task<IActionResult> EditProduct(int id)
		{
			var spec = new ProductFullInfo(id);

			var product = await _productRepository.GetEntityWithSpec(spec);

			return View(_mapper.Map<Product, ProductEditViewModel>(product));
		}

		[HttpPost("edit/{id}")]
		public async Task<IActionResult> EditProduct(ProductEditViewModel productToAdd)
		{
			if (!ModelState.IsValid)
				return View(productToAdd);

			var product = _mapper.Map<ProductEditViewModel, Product>(productToAdd);

			_productRepository.Update(product);
			await _productRepository.SaveAsync();

			return RedirectToAction("Index");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);

			if (product is null)
				return NotFound();

			product.IsDeleted = true;
			_productRepository.Update(product);
			await _productRepository.SaveAsync();

			return Ok();
		}
	}
}
