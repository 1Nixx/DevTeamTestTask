using System.ComponentModel.DataAnnotations;
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
		public string Price { get; set; }

		[Required]
		[RegularExpression("#[0-9A-Fa-f]{6}$")]
		public string Color { get; set; }

		[Required]
		public int ShopId { get; set; }
	}
}
