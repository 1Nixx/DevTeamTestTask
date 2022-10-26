using Core.Entities;
using Web.ViewModels.Shop;

namespace Web.ViewModels.Product
{
	public class ProductFullViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
		public string Color { get; set; }
		public ShopFullViewModel Shop { get; set; }
	}
}
