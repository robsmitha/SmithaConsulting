using DomainLayer.Models;
using AutoMapper;
using DataLayer;

namespace DomainLayer.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Application, ApplicationModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<LineItem, LineItemModel>().ReverseMap();
            CreateMap<Theme, ThemeModel>().ReverseMap();
            CreateMap<Item, ItemModel>().ReverseMap();

            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>().ForMember(m => m.OrderStatusType, opt => opt.Ignore());
            CreateMap<Merchant, MerchantModel>();
            CreateMap<MerchantModel, Merchant>().ForMember(m => m.MerchantType, opt => opt.Ignore());
            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>().ForMember(m => m.PaymentType, opt => opt.Ignore());
        }
    }
}
