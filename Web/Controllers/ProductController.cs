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

		[HttpGet("")]
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
			
			return View(_mapper.Map<Product, ProductViewModel>(product));
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
			return View("AddEditProduct");
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddNewProduct(ProductAddViewModel productToAdd)
		{
			var product = _mapper.Map<ProductAddViewModel, Product>(productToAdd);

			await _productRepository.AddAsync(product);
			await _productRepository.SaveAsync();

			return Ok();
		}

		[HttpGet("edit")]
		public async Task<IActionResult> EditProduc(int id)
		{
			var spec = new ProductFullInfo(id);

			var product = await _productRepository.GetEntityWithSpec(spec);

			return View("AddEditProduct", _mapper.Map<Product, ProductViewModel>(product));
		}

		[HttpPost("edit")]
		public async Task<IActionResult> UpdateProduct(ProductEditViewModel productToAdd)
		{
			var product = _mapper.Map<ProductEditViewModel, Product>(productToAdd);

			_productRepository.Update(product);
			await _productRepository.SaveAsync();

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);

			_productRepository.Delete(product);
			await _productRepository.SaveAsync();

			return Ok();
		}
	}
}
