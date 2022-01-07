using Banky.Domain.Interfaces;
using Banky.EfCore.Contexts;
using Banky.EfCore.Repositories;

namespace Banky.EfCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Accounts = new AccountRepository(_context);
            Details = new DetailRepository(_context);
            History = new HistoryRepository(_context);
        }
        public IUserRepository Users { get;}
        public IAccountRepository Accounts { get;}
        public IHistoryRepository History { get;}
        public IDetailRepository Details { get;}

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();

    }
}
