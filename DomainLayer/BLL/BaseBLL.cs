using AutoMapper;
using DataLayer.DAL;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.BLL
{
    public class BaseBLL
    {
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseBLL(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
