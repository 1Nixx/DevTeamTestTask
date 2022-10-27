using Core.Entities;

namespace Core.Specification
{
    public class OrdersFilteredByStatus : BaseSpecification<Order>
	{
		public OrdersFilteredByStatus(OrderSpecParams productParams)
			: base()
		{
			if (productParams.ListType is not null)
				AddCriteria(x => x.Status == productParams.ListType.Value);
		}
	}
}
