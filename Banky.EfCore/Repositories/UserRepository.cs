using Banky.Domain.Entities;
using Banky.Domain.Interfaces;
using Banky.EfCore.Contexts;
using System.Linq.Expressions;

namespace Banky.EfCore.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) => Expression.Empty();

        
    }
}
