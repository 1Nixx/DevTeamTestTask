using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
	public class BaseSpecification<T> : ISpecification<T>
	{
		public BaseSpecification()
		{
		}

		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public Expression<Func<T, bool>> Criteria { get; private set; }

		public List<Expression<Func<T, object>>> Includes { get; } = new();

		protected void AddInclude(Expression<Func<T, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}

		protected void AddCriteria(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}
	}
}
