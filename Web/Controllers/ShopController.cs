using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private readonly IShopService _shopService;
		private readonly IMapper _mapper;

		public ShopController(IShopService shopService, IMapper mapper)
		{
			_shopService = shopService;
			_mapper = mapper;
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<ShopShortViewModel>> GetAllShops()
		{
			var shops = await _shopService.GetAllShops();

			return _mapper.Map<IReadOnlyList<Shop>, IReadOnlyList<ShopShortViewModel>>(shops);
		}
	}
}
