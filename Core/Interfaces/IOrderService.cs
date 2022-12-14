using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
	public interface IOrderService
	{
		Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderSpecParams filterParams);
		Task<Order> GetOrderByIdAsync(int id);
		Task UpdateOrderStatus(int id, OrderStatus orderStatus);
	}
}
