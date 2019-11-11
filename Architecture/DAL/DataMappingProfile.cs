using Architecture.Models;
using AutoMapper;
using DataModeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.DAL
{
    public class DataMappingProfile  : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<Customer, CustomerModel>();
        }
    }
}
