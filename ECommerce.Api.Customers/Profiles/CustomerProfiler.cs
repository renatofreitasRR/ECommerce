using AutoMapper;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfiler : Profile
    {
        public CustomerProfiler()
        {
            CreateMap<Entities.Customer, Models.Customer>();
        }
    }
}
