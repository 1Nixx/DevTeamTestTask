using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Order;

namespace Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IMapper _mapper;
		public OrderController(IGenericRepository<Order> orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<ShortOrderViewModel>> GetOrders([FromQuery]OrderSpecParams orderParams)
		{
			var spec = new OrdersFilteredByStatus(orderParams);

			var orders = await _orderRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<ShortOrderViewModel>>(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderViewModel>> OrderByid(int id)
		{
			var spec = new OrderFullInfo(id);

			var order = await _orderRepository.GetEntityWithSpec(spec);

			if (order == null) return NotFound();

			return _mapper.Map<Order, OrderViewModel>(order);
		}

		[Authorize("Admin")]
		[HttpPost("changestatus/{id:int}")]
		public async Task<IActionResult> ChangeStatus(int id, TypeOrderViewModel newOrderStatus)
		{
			var order = await _orderRepository.GetByIdAsync(id);

			order.Status = _mapper.Map<TypeOrderViewModel, OrderStatus>(newOrderStatus);

			_orderRepository.Update(order);
			await _orderRepository.SaveAsync();

			return Ok();
		}
	}
}