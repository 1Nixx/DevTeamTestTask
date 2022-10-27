using Core.Entities;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IGenericRepository<Order> _orderRepository;
		public OrderService(IGenericRepository<Order> orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderSpecParams filterParams)
		{
			var spec = new OrdersFilteredByStatus(filterParams);
			var orders = await _orderRepository.ListAsync(spec);
			return orders;
		}

		public async Task<Order> GetOrderByIdAsync(int id)
		{
			var spec = new OrderFullInfoById(id);
			var order = await _orderRepository.GetEntityWithSpec(spec);
			return order;
		}

		public async Task UpdateOrderStatus(int id, OrderStatus orderStatus)
		{
			var order = await _orderRepository.GetByIdAsync(id);

			if (order is null)
				throw new NullReferenceException();

			order.Status = orderStatus;
			_orderRepository.Update(order);
			await _orderRepository.SaveAsync();
		}
	}
}
