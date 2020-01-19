using AutoMapper;
using Domain.Models;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Utilities
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<ItemModel, ItemViewModel>().ReverseMap();

        }
    }
}
