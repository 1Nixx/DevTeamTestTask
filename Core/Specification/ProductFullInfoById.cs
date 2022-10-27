using Core.Entities;

namespace Core.Specification
{
	public class ProductFullInfoById : BaseSpecification<Product>
	{
		public ProductFullInfoById(int id) : base(x => 
			x.Id == id)
		{
			AddInclude(x => x.Shop);
		}
	}
}
