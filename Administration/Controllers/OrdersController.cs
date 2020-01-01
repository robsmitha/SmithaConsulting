using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DomainLayer.Entities;

namespace Administration.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly OperationsContext _context;

        public OrdersController(OperationsContext context) : base(context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? merchantId)
        {
            merchantId = merchantId ?? MerchantID;
            var merchantOrders = _context.Orders
                .Where(x => x.MerchantID == merchantId)
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User);
            return View(await merchantOrders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = GetOrderViewModel(order);
            return View(orderViewModel);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Email");
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName");
            ViewData["OrderStatusTypeID"] = new SelectList(_context.OrderStatusTypes, "ID", "Description");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Note,Total,OrderStatusTypeID,MerchantID,CustomerID,UserID,ID,CreatedAt,Active,ModifiedTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Email", order.CustomerID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", order.MerchantID);
            ViewData["OrderStatusTypeID"] = new SelectList(_context.OrderStatusTypes, "ID", "Description", order.OrderStatusTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", order.UserID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Email", order.CustomerID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", order.MerchantID);
            ViewData["OrderStatusTypeID"] = new SelectList(_context.OrderStatusTypes, "ID", "Description", order.OrderStatusTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Note,Total,OrderStatusTypeID,MerchantID,CustomerID,UserID,ID,CreatedAt,Active,ModifiedTime")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Email", order.CustomerID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", order.MerchantID);
            ViewData["OrderStatusTypeID"] = new SelectList(_context.OrderStatusTypes, "ID", "Description", order.OrderStatusTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
