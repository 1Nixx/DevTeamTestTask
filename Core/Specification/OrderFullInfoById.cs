using Core.Entities;

namespace Core.Specification
{
    public class OrderFullInfoById : BaseSpecification<Order>
	{
		public OrderFullInfoById(int id)
			: base(x => x.Id == id)
		{
			AddInclude(x => x.Client);
			AddInclude(x => x.Products);
		}
	}
}
