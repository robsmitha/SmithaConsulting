using DataModeling;
using Architecture.DTOs;
using Architecture.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class RegisterListViewModel
    {
        public List<ItemDTO> Items { get; set; }
        public RegisterListViewModel(List<ItemDTO> items)
        {
            Items = items;
        }
    }
}
