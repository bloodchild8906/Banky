
namespace Banky.Dto.V1.Document.Request
{
    public class TransferMoneyRequest
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
