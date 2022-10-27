using System.ComponentModel.DataAnnotations;
using Web.Validators;

namespace Web.ViewModels.Product
{
	public class ProductAddViewModel
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[PriceValidator]
		public string Price { get; set; }

		[Required]
		[ColorValidator]
		public string Color { get; set; }

		[Required]
		public int ShopId { get; set; }
	}
}
