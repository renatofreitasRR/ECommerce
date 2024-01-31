using AutoMapper;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Entities.Order, Models.Order>();
            CreateMap<Entities.OrderItem, Models.OrderItem>();
        }

    }
}
