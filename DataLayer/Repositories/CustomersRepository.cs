using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class CustomersRepository : GenericRepository<Customer>
    {
        public CustomersRepository(DbArchitecture context) : base(context) { }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await GetAllAsync();
        }
    }
}
