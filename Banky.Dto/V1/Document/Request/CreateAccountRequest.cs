
namespace Banky.Dto.V1.Document.Request
{
    public class CreateAccountRequest
    {
        public string AccountNumber { get; set; }
        public decimal initialDepositAmount { get; set; }
    }
}
