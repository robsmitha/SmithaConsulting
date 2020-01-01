﻿using DataLayer.Data;
using DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class CustomersRepository : GenericRepository<Customer>
    {
        public CustomersRepository(OperationsContext context) : base(context) { }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await GetAllAsync();
        }
    }
}
