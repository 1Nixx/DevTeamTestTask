using System.Linq.Expressions;

namespace Core.Specification
{
	public interface ISpecification<T>
	{
		Expression<Func<T, bool>> Criteria { get; }
		List<Expression<Func<T, object>>> Includes { get; }
		int Take { get; }
		int Skip { get; }
		bool IsPagingEnabled { get; }
	}
}
