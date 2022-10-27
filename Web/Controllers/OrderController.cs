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
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<OrderShortViewModel>> GetOrders([FromQuery] OrderSpecParams orderParams)
		{
			var orders = await _orderService.GetAllOrdersAsync(orderParams);
			return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderShortViewModel>>(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderFullViewModel>> Order(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order is null) return NotFound();

			return View(_mapper.Map<Order, OrderFullViewModel>(order));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("changestatus/{id}")]
		public async Task<IActionResult> ChangeStatus(int id, OrderTypeViewModel newOrderStatus)
		{
			var orderStatus = _mapper.Map<OrderTypeViewModel, OrderStatus>(newOrderStatus);
			try
			{
				await _orderService.UpdateOrderStatus(id, orderStatus);
			}
			catch (NullReferenceException)
			{
				return NotFound();
			}
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