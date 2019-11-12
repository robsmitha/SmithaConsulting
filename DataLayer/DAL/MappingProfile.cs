using DataLayer.Models;
using AutoMapper;

namespace DataLayer.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Application, ApplicationModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>().ForMember(m => m.OrderStatusType, opt => opt.Ignore());
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<LineItem, LineItemModel>().ReverseMap();
        }
    }
}
