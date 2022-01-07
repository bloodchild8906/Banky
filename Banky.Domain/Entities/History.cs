namespace Banky.Domain.Entities
{
    public class History
    {
        public long Id { get; set; }
        public string FromAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public SourceType TransactionType { get; set; }
        public string DestinationAccountNumber { get; set; }

    }
    public enum SourceType { Withdrawl,Deposit,Transfer }
}
