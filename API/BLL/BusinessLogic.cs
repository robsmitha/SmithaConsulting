using AutoMapper;
using DataLayer.Data;
using DataLayer.Repositories;

namespace API.BLL
{
    public class BusinessLogic
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessLogic(OperationsContext context, IMapper mapper)
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(context);
            }
            _mapper = mapper;
        }

        private CustomersBLL customers;
        public CustomersBLL Customers
        {
            get => customers = customers ?? new CustomersBLL(_unitOfWork, _mapper);
            set => customers = value;
        }

        private OrdersBLL orders;
        public OrdersBLL Orders
        {
            get => orders = orders ?? new OrdersBLL(_unitOfWork, _mapper);
            set => orders = value;
        }

        private ApplicationsBLL applications;
        public ApplicationsBLL Applications
        {
            get => applications = applications ?? new ApplicationsBLL(_unitOfWork, _mapper);
            set => applications = value;
        }
        private BlogBLL blogs;
        public BlogBLL Blogs
        {
            get => blogs = blogs ?? new BlogBLL(_unitOfWork, _mapper);
            set => blogs = value;
        }
        private ItemsBLL items;
        public ItemsBLL Items
        {
            get => items = items ?? new ItemsBLL(_unitOfWork, _mapper);
            set => items = value;
        }
        private LineItemsBLL lineItems;
        public LineItemsBLL LineItems
        {
            get => lineItems = lineItems ?? new LineItemsBLL(_unitOfWork, _mapper);
            set => lineItems = value;
        }
        private MerchantsBLL merchants;
        public MerchantsBLL Merchants
        {
            get => merchants = merchants ?? new MerchantsBLL(_unitOfWork, _mapper);
            set => merchants = value;
        }
        private PaymentsBLL payments;
        public PaymentsBLL Payments
        {
            get => payments = payments ?? new PaymentsBLL(_unitOfWork, _mapper);
            set => payments = value;
        }
        private ThemesBLL themes;
        public ThemesBLL Themes
        {
            get => themes = themes ?? new ThemesBLL(_unitOfWork, _mapper);
            set => themes = value;
        }
    }
}
