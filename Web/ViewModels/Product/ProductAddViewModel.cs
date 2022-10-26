using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.ViewModels.Product
{
	public class ProductAddViewModel
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Color { get; set; }
		public int ShopId { get; set; }
	}
}
