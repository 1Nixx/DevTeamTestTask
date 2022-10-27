using Core.Entities;

namespace Core.Specification
{
	public class ProductsFilteredByShop : BaseSpecification<Product>
	{
		public ProductsFilteredByShop(ProductSpecParam specParam) : base()
		{
			if (specParam.ShopId is not null)
				AddCriteria(x => x.ShopId == specParam.ShopId && x.IsDeleted == false);
			else
				AddCriteria(x => x.IsDeleted == false);
		}
	}
}
