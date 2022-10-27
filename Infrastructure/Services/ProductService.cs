using Core.Entities;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<Product> _productRepository;
		public ProductService(IGenericRepository<Product> productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			var spec = new ProductFullInfoById(id);
			var product = await _productRepository.GetEntityWithSpec(spec);
			return product;
		}

		public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParam specParam)
		{
			var spec = new ProductsFilteredByShop(specParam);
			var products = await _productRepository.ListAsync(spec);
			return products;
		}

		public async Task AddProductAsync(Product product)
		{
			await _productRepository.AddAsync(product);
			await _productRepository.SaveAsync();
		}

		public async Task UpdateProductAsync(Product product)
		{
			_productRepository.Update(product);
			await _productRepository.SaveAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);

			if (product is null)
				throw new NullReferenceException();

			product.IsDeleted = true;
			_productRepository.Update(product);
			await _productRepository.SaveAsync();
		}
	}
}
