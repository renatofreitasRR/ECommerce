using AutoMapper;
using ECommerce.Api.Products;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Models.Product>();
        }
    }
}
