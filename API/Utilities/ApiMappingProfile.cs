using Domain.Models;
using AutoMapper;
using Domain.Entities;

namespace API.Utilities
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Application, ApplicationModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Theme, ThemeModel>().ReverseMap();
            CreateMap<Item, ItemModel>().ReverseMap();

            CreateMap<LineItem, LineItemModel>()
                .ForMember(m => m.ItemName, opt => opt.MapFrom(src => src.Item.ItemName))
                .ForMember(m => m.ItemTypeID, opt => opt.MapFrom(src => src.Item.ItemTypeID))
                .ForMember(m => m.ItemDescription, opt => opt.MapFrom(src => src.Item.ItemDescription));
            CreateMap<LineItemModel, LineItem>().ForMember(m => m.Item, opt => opt.Ignore());
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>().ForMember(m => m.OrderStatusType, opt => opt.Ignore());
            CreateMap<Merchant, MerchantModel>();
            CreateMap<MerchantModel, Merchant>().ForMember(m => m.MerchantType, opt => opt.Ignore());
            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>().ForMember(m => m.PaymentType, opt => opt.Ignore());
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<ItemCategoryType, ItemCategoryTypeModel>().ReverseMap();
        }
    }
}
