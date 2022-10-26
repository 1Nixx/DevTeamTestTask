using Core.Entities;

namespace Core.Specification
{
	public class ProductFullInfo : BaseSpecification<Product>
	{
		public ProductFullInfo(int id) : base(x => 
			x.Id == id)
		{
			AddInclude(x => x.Shop);
		}
	}
}
