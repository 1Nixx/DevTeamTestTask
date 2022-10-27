using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
	public interface IProductService
	{
		Task<Product> GetProductByIdAsync(int id);
		Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParam specParam);
		Task AddProductAsync(Product product);
		Task UpdateProductAsync(Product product);
		Task DeleteProductAsync(int id);
	}
}
