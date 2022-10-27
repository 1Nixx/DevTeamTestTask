using Core.Entities;

namespace Core.Interfaces
{
	public interface IShopService
	{
		Task<IReadOnlyList<Shop>> GetAllShops();
	}
}
