using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.ViewModels.Product
{
	public class ProductAddViewModel
	{
		public int Name { get; set; }
		public decimal Price { get; set; }
		public string Color { get; set; }
		public int ShopId { get; set; }
	}
}
