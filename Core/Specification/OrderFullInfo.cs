using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class OrderFullInfo : BaseSpecification<Order>
	{
		public OrderFullInfo(int id)
			: base(x => x.Id == id)
		{
			AddInclude(x => x.Client);
			AddInclude(x => x.Products);
		}
	}
}
