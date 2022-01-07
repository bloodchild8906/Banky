using System.Collections.Generic;

namespace Banky.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public virtual List<Account> Accounts { get; set; } = new();
        public virtual List<History> AccountHistory { get; set; } = new();

        public virtual Detail Detail { get; set; }

    }
}
