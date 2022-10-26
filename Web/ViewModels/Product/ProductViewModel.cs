using Core.Entities;
using Web.ViewModels.Shop;

namespace Web.ViewModels.Product
{
	public class ProductViewModel
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Color { get; set; }
		public ShopViewModel Shop { get; set; }
		public bool IsDeleted { get; set; }
	}
}
