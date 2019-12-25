using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbArchitecture _context;
        public UnitOfWork(DbArchitecture context)
        {
            _context = context;
        }

        #region Repositories
        private OrdersRepository orderRepository;
        public OrdersRepository OrderRepository
        {
            get => orderRepository = orderRepository ?? new OrdersRepository(_context);
            set => orderRepository = value;
        }
        private GenericRepository<LineItem> lineItemRepository;
        public GenericRepository<LineItem> LineItemRepository
        {
            get => lineItemRepository = lineItemRepository ?? new GenericRepository<LineItem>(_context);
            set => lineItemRepository = value;
        }
        private GenericRepository<Blog> blogRepository;
        public GenericRepository<Blog> BlogRepository
        {
            get => blogRepository = blogRepository ?? new GenericRepository<Blog>(_context);
            set => blogRepository = value;
        }
        private GenericRepository<Application> applicationRepository;
        public GenericRepository<Application> ApplicationRepository
        {
            get => applicationRepository = applicationRepository ?? new GenericRepository<Application>(_context);
            set => applicationRepository = value;
        }
        private CustomersRepository customerRepository;
        public CustomersRepository CustomerRepository
        {
            get => customerRepository = customerRepository ?? new CustomersRepository(_context);
            set => customerRepository = value;
        }
        private GenericRepository<Item> itemRepository;
        public GenericRepository<Item> ItemRepository
        {
            get => itemRepository = itemRepository ?? new GenericRepository<Item>(_context);
            set => itemRepository = value;
        }
        private MerchantsRepository merchantsRepository;
        public MerchantsRepository MerchantRepository
        {
            get => merchantsRepository = merchantsRepository ?? new MerchantsRepository(_context);
            set => merchantsRepository = value;
        }
        private GenericRepository<Payment> paymentRepository;
        public GenericRepository<Payment> PaymentRepository
        {
            get => paymentRepository = paymentRepository ?? new GenericRepository<Payment>(_context);
            set => paymentRepository = value;
        }
        private GenericRepository<Theme> themeRepository;
        public GenericRepository<Theme> ThemeRepository
        {
            get => themeRepository = themeRepository ?? new GenericRepository<Theme>(_context);
            set => themeRepository = value;
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
