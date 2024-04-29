using AutoMapper;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity_Entities;
using Talabat.Core.Entities.Order;


namespace Talabat.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(S => S.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<Core.Entities.Identity_Entities.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemsDto, BasketItem>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.Order.Address>();

            CreateMap<Orders, OrderToReturnDto>()
                .ForMember(d => d.DelivaryMethod, o => o.MapFrom(S => S.DelivaryMethod.ShortName))
                .ForMember(d => d.DelivaryMethodCost, o => o.MapFrom(S => S.DelivaryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(S => S.product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(S => S.product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
