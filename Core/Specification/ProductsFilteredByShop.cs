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
		public ProductsFilteredByShop(ProductSpecParam specParam) : base(x => 
			x.ShopId == specParam.ShopId && x.IsDeleted == false)
		{

		}
	}
}
