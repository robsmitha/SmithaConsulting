using System;
using DataModeling;
using DataModeling.Data;

namespace Architecture.DAL
{
    public class UnitOfWork : IDisposable
    {
        private DbArchitecture _context;
        public UnitOfWork(DbArchitecture context)
        {
            _context = context;
        }
        private GenericRepository<Order> orderRepository;
        public GenericRepository<Order> OrderRepository
        {
            get => orderRepository = orderRepository ?? new GenericRepository<Order>(_context);
            set => orderRepository = value;
        }
        public void Save()
        {
            _context.SaveChanges();
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
