using System.ComponentModel.DataAnnotations;
using Web.Validators;

namespace Web.ViewModels.Product
{
	public class ProductEditViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[PriceValidator]
		public string Price { get; set; }

		[Required]
		[RegularExpression("#[0-9A-Fa-f]{6}$")]
		public string Color { get; set; }

		[Required]
		public int ShopId { get; set; }
	}
}
