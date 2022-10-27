using AutoMapper;
using Core.Entities;
using Web.ViewModels.Client;
using Web.ViewModels.Order;
using Web.ViewModels.Product;
using Web.ViewModels.Shop;

namespace Web.Helpers
{
    public class MappingProfiles : Profile
    {
		public MappingProfiles()
		{
			CreateMap<Order, OrderShortViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.OrderType, o => o.MapFrom(s => s.Status));

			CreateMap<Order, OrderFullViewModel>()
				.ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.OrderType, o => o.MapFrom(s => s.Status))
				.ForMember(d => d.Client, o => o.MapFrom(s => s.Client))
				.ForMember(d => d.Products, o => o.MapFrom(s => s.Products));

			CreateMap<Client, ClientViewModel>()
				.ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Address, o => o.MapFrom(s => s.Address));

			CreateMap<Product, ProductShortViewModel>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Price, o => o.MapFrom(s => PriceFormatter.Format(s.Price)));

			CreateMap<Shop, ShopShortViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

			CreateMap<Product, ProductFullViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Price, o => o.MapFrom(s => PriceFormatter.Format(s.Price)))
				.ForMember(d => d.Color, o => o.MapFrom(s => s.Color));

			CreateMap<Shop, ShopFullViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

			CreateMap<ProductEditViewModel, Product>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Price, o => o.MapFrom(s => PriceFormatter.ToDecimal(s.Price)))
				.ForMember(d => d.Color, o => o.MapFrom(s => s.Color))
				.ForMember(d => d.ShopId, o => o.MapFrom(s => s.ShopId));

			CreateMap<Product, ProductEditViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Price, o => o.MapFrom(s => PriceFormatter.Format(s.Price)))
				.ForMember(d => d.Color, o => o.MapFrom(s => s.Color))
				.ForMember(d => d.ShopId, o => o.MapFrom(s => s.ShopId));

			CreateMap<ProductAddViewModel, Product>()
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
				.ForMember(d => d.Price, o => o.MapFrom(s => PriceFormatter.ToDecimal(s.Price)))
				.ForMember(d => d.Color, o => o.MapFrom(s => s.Color))
				.ForMember(d => d.ShopId, o => o.MapFrom(s => s.ShopId));
		}

	}
}
