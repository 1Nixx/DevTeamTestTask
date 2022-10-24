namespace Core.Entities
{
	public class Shop : BaseEntity
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public List<Product> Products { get; set; }
	}
}
