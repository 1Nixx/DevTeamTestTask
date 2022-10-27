using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
	public class ShopService : IShopService
	{
		private readonly IGenericRepository<Shop> _shopRepository;
		public ShopService(IGenericRepository<Shop> shopRepository)
		{
			_shopRepository = shopRepository;
		}

		public async Task<IReadOnlyList<Shop>> GetAllShops()
		{
			return await _shopRepository.ListAllAsync();
		}
	}
}
