using AutoMapper;
using DataLayer.DAL;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.BLL
{
    public class BusinessLogic
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessLogic(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
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
    }
}
