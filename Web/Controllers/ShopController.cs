using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		private readonly IGenericRepository<Shop> _shopRepository;
		private readonly IMapper _mapper;
		public ShopController(IGenericRepository<Shop> shopRepository, IMapper mapper)
		{
			_shopRepository = shopRepository;
			_mapper = mapper;
		}

		[HttpGet("all")]
		public async Task<IReadOnlyList<ShopShortViewModel>> GetAllShops()
		{
			var shops = await _shopRepository.ListAllAsync();

			return _mapper.Map<IReadOnlyList<Shop>, IReadOnlyList<ShopShortViewModel>>(shops);
		}
	}
}
