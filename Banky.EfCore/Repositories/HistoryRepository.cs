using Banky.Domain.Entities;
using Banky.Domain.Interfaces;
using Banky.EfCore.Contexts;
using System.Linq.Expressions;

namespace Banky.EfCore.Repositories
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        public HistoryRepository(ApplicationContext context) : base(context) => Expression.Empty();


    }
}
