using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.ViewModels.Order;

namespace Web.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class OrderController : Controller
	{
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IMapper _mapper;
		public OrderController(IGenericRepository<Order> orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<OrderShortViewModel>> GetOrders([FromQuery]OrderSpecParams orderParams)
		{
			var spec = new OrdersFilteredByStatus(orderParams);

			var orders = await _orderRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderShortViewModel>>(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderFullViewModel>> Order(int id)
		{
			var spec = new OrderFullInfo(id);

			var order = await _orderRepository.GetEntityWithSpec(spec);

			if (order == null) return NotFound();

			return View(_mapper.Map<Order, OrderFullViewModel>(order));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("changestatus/{id}")]
		public async Task<IActionResult> ChangeStatus(int id, OrderTypeViewModel newOrderStatus)
		{
			var order = await _orderRepository.GetByIdAsync(id);

			order.Status = _mapper.Map<OrderTypeViewModel, OrderStatus>(newOrderStatus);

			_orderRepository.Update(order);
			await _orderRepository.SaveAsync();

			return Ok();
		}

		[HttpGet("orderstatus")]
		public ActionResult<List<StatusNameViewModel>> GetOrderStatus()
		{
			var list = new List<StatusNameViewModel>();

			foreach (var status in OrderStatusName.StatusNames)
			{
				list.Add(new StatusNameViewModel
				{
					Id = status.Key,
					StatusName = status.Value
				});
			}

			return Json(list);
		}
	}
}