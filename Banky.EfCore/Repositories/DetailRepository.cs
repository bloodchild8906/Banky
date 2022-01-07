using Banky.Domain.Entities;
using Banky.Domain.Interfaces;
using Banky.EfCore.Contexts;
using System.Linq.Expressions;

namespace Banky.EfCore.Repositories
{
    public class DetailRepository : GenericRepository<Detail>, IDetailRepository
    {
        public DetailRepository(ApplicationContext context) : base(context) => Expression.Empty();

        
    }
}
