using Core.Entities;

namespace Infrastructure.Data
{
	public class MarketContextSeed
	{
		public static async Task SeedAsync(MarketContext context)
		{
			if (context.Orders.Any())
				return;

			await context.AddRangeAsync(new Shop
			{
				Name = "Magazin Pil",
				Address = "Gomel Angeles"
			},
			new Shop
			{
				Name = "Shop #666",
				Address = "Minsk"
			},
			new Shop
			{
				Name = "Bookshop",
				Address = "Poland Warsaw"
			});

			await context.SaveChangesAsync();

			var shop1 = context.Shops.Where(x => x.Name == "Magazin Pil").Select(s => s).FirstOrDefault();
			var shop2 = context.Shops.Where(x => x.Name == "Shop #666").Select(s => s).FirstOrDefault();

			await context.AddAsync(new Client
			{
				Name = "Nikita Hripach",
				Address = "г. Минск, ул. Петруся Бровки, 3",
				Orders = new List<Order>()
				{
					new Order
					{
						Status = OrderStatus.Active,
						Products = new List<Product>()
						{
							new Product
							{
								Name = "Pila",
								Price = 45.99M,
								Color = "#788845",
								IsDeleted = false,
								Shop = shop1
							},
							new Product
							{
								Name = "Pila #2",
								Price = 1000.99M,
								Color = "#598648",
								IsDeleted = false,
								Shop = shop1
							}
						}
					}
				}
			});

			await context.SaveChangesAsync();

			await context.AddAsync(new Client
			{
				Name = "Mihael Mikle",
				Address = "г. Гомель, ул. Якубовского, 3",
				Orders = new List<Order>()
				{
					new Order
					{
						Status = OrderStatus.Active,
						Products = new List<Product>()
						{
							new Product
							{
								Name = "Test2",
								Price = 1M,
								Color = "#956684",
								IsDeleted = false,
								Shop = shop2
							},
							new Product
							{
								Name = "Pila 34534",
								Price = 111111.99M,
								Color = "#963255",
								IsDeleted = false,
								Shop = shop1
							},
							context.Products.Where(x => x.Name == "Pila #2").Select(s => s).FirstOrDefault()
						}
					}
				}
			}) ;

			await context.SaveChangesAsync();

			await context.AddAsync(new Order
			{
				Client = context.Clients.Where(x => x.Name == "Nikita Hripach").Select(s => s).FirstOrDefault(),
				Products = new List<Product>()
				{
					new Product
					{
						Name = "Test 56565",
						Price = 5555M,
						Color = "#789456",
						IsDeleted = true,
						Shop = context.Shops.Where(x => x.Name == "Shop #666").Select(s => s).FirstOrDefault()
					},
					context.Products.Where(x => x.Name == "Pila #2").Select(s => s).FirstOrDefault()
				}
			});

			await context.AddAsync(new Order
			{
				Client = context.Clients.Where(x => x.Name == "Nikita Hripach").Select(s => s).FirstOrDefault(),
				Products = new List<Product>()
				{
					new Product
					{
						Name = "Big toy",
						Price = 5588.55M,
						Color = "#88FF44",
						IsDeleted = true,
						Shop = context.Shops.Where(x => x.Name == "Shop #666").Select(s => s).FirstOrDefault()
					},
					context.Products.Where(x => x.Name == "Pila 34534").Select(s => s).FirstOrDefault()
				},
				Status = OrderStatus.Completed
			});

			await context.SaveChangesAsync();
		}
	}
}
