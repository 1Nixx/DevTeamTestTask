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
			: base(x =>
				productParams.ListType.HasValue && x.Status == productParams.ListType.Value)
		{

		}
	}
}
