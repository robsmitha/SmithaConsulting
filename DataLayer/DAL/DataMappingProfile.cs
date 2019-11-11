using DataLayer.Models;
using AutoMapper;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DAL
{
    public class DataMappingProfile  : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<Customer, CustomerModel>();
        }
    }
}
