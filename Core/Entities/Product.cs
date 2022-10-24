﻿namespace Core.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Color { get; set; }
		public List<Order> Orders { get; set; }
		public Shop Shop { get; set; }
		public bool IsDeleted { get; set; }
	}
}