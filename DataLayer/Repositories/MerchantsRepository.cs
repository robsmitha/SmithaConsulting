using DataLayer.Data;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class MerchantsRepository : GenericRepository<Merchant>
    {
        public MerchantsRepository(OperationsContext context) : base(context) { }

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
