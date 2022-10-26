using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
