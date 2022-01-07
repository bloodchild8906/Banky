using System;

namespace Banky.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get;  }
        IAccountRepository Accounts { get;  }
        IHistoryRepository History { get; }
        IDetailRepository Details { get; }
        int Complete();
    }
}
