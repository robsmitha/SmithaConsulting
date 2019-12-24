using AutoMapper;
using DomainLayer.Models;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Utilities
{
    public class PortfolioMappingProfile : Profile
    {
        public PortfolioMappingProfile()
        {
            CreateMap<BlogModel, BlogViewModel>().ReverseMap();
        }
    }
}
