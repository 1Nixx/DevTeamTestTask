namespace Core.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public OrderStatus Status { get; set; }
		public Client Client { get; set; }
		public List<Product> Products { get; set; }
	}

	public enum OrderStatus
	{
		Active,
		Completed
	}
}