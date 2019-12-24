using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL
{
    public class MerchantsRepository : GenericRepository<Merchant>
    {
        public MerchantsRepository(DbArchitecture context) : base(context) { }

        public List<Item> GetMerchantItems(int id) => context.Items
            .Include(o => o.Merchant)
            .Where(i => i.MerchantID == id)
            .ToList();
        public async Task<List<Item>> GetMerchantItemsAsync(int id) => await context.Items
            .Include(o => o.Merchant)
            .Where(i => i.MerchantID == id)
            .ToListAsync();
    }
}
