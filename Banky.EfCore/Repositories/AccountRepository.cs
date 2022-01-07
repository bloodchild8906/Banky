using Banky.Domain.Entities;
using Banky.Domain.Interfaces;
using Banky.EfCore.Contexts;
using System.Linq.Expressions;

namespace Banky.EfCore.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationContext context) : base(context) => Expression.Empty();

        
    }
}
