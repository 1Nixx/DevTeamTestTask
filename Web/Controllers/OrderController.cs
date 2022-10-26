using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
		public async Task<IReadOnlyList<ShortOrderViewModel>> GetOrders([FromQuery]OrderSpecParams orderParams)
		{
			var spec = new OrdersFilteredByStatus(orderParams);

			var orders = await _orderRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<ShortOrderViewModel>>(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderViewModel>> Order(int id)
		{
			var spec = new OrderFullInfo(id);

			var order = await _orderRepository.GetEntityWithSpec(spec);

			if (order == null) return NotFound();

			return View(_mapper.Map<Order, OrderViewModel>(order));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("changestatus/{id}")]
		public async Task<IActionResult> ChangeStatus(int id, TypeOrderViewModel newOrderStatus)
		{
			var order = await _orderRepository.GetByIdAsync(id);

			order.Status = _mapper.Map<TypeOrderViewModel, OrderStatus>(newOrderStatus);

			_orderRepository.Update(order);
			await _orderRepository.SaveAsync();

			return Ok();
		}

		[HttpGet("orderstatus")]
		public ActionResult<List<OrderStatusName>> GetOrderStatus()
		{
			var list = new List<OrderStatusName>();

			//var enumLength = Enum.GetNames(typeof(TypeOrderViewModel)).Length;

			//for (int i = 0; i < enumLength; i++)
			//	list.Add((i, ));

			list.Add(new OrderStatusName{
				id = 0,
				StatusName = "Активные" 
			});
			list.Add(new OrderStatusName
			{
				id = 1,
				StatusName = "Выполненные"
			});

			return Json(list);
		}
	}
}