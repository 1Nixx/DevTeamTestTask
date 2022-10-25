using AutoMapper;
using Core.Entities;
using Web.ViewModels.Order;

namespace Web.Helpers
{
    public class MappingProfiles : Profile
    {
		public MappingProfiles()
		{
			CreateMap<Order, ShortOrderViewModel>();
			CreateMap<Order, OrderViewModel>();
			CreateMap<TypeOrderViewModel, OrderStatus>();
		}

	}
}
