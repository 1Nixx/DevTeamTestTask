using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
