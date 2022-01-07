using Banky.Domain.Entities;
using Banky.Domain.Interfaces;
using Banky.Dto.V1.Document.Request;
using Banky.Dto.V1.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Banky.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration) => _unitOfWork = unitOfWork;
        


        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var docs = tmpUser.Accounts;
            return Ok(docs);
        }

        [HttpGet]
        public ActionResult Get([FromQuery(Name = "accountNumber")] string accountNumber)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var account = tmpUser.Accounts.Find(acc=>acc.AccountNumber== accountNumber);
            return Ok(account);
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create([FromBody] CreateAccountRequest createAccountRequest)
        {
            var valid = new CreateAccountValidator();
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            if (valid.GetValidation(createAccountRequest.AccountNumber).HasError)
                return BadRequest(valid.Errors);
            if (_unitOfWork.Accounts.Find(acc => acc.AccountNumber == createAccountRequest.AccountNumber).ToList().Count > 0)
                return BadRequest("an account with that account number already exsists");
            

            var acc = new Account()
            {
                AccountNumber = createAccountRequest.AccountNumber,
                Balance =createAccountRequest.initialDepositAmount
            };

            tmpUser.Accounts.Add(acc);
            _unitOfWork.Accounts.Add(acc);
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Complete();
            return Ok();
        }

        [Route("Deposit")]
        [HttpPut]
        public ActionResult Deposit([FromBody] ModifyAccountRequest modifyAccountRequest)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var accounts = tmpUser.Accounts;
            var modified = accounts.Where(acc => acc.AccountNumber == modifyAccountRequest.AccountNumber).FirstOrDefault();
            modified.Balance += modifyAccountRequest.Amount;
            _unitOfWork.Accounts.Update(modified);
            tmpUser.AccountHistory.Add(new History()
            {
                FromAccountNumber = modified.AccountNumber,
                Amount = modifyAccountRequest.Amount,
                DestinationAccountNumber = modified.AccountNumber,
                TransactionType = SourceType.Deposit
            });
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Complete();
            return Ok();
        }
        [Route("Withdraw")]
        [HttpPut]
        public ActionResult Withdraw([FromBody] ModifyAccountRequest modifyAccountRequest)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var accounts = tmpUser.Accounts;
            var modified = accounts.Where(acc => acc.AccountNumber == modifyAccountRequest.AccountNumber).FirstOrDefault();
            if(!((modified.Balance - modifyAccountRequest.Amount)>=0)) return BadRequest("you cannot withdraw more than you own");
            modified.Balance -= modifyAccountRequest.Amount;
            _unitOfWork.Accounts.Update(modified);
            tmpUser.AccountHistory.Add(new History()
            {
                FromAccountNumber = modified.AccountNumber,
                Amount = modifyAccountRequest.Amount*-1,
                DestinationAccountNumber = modified.AccountNumber,
                TransactionType = SourceType.Withdrawl
            });
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Complete();
            return Ok();
        }

        [Route("Transfer")]
        [HttpPut]
        public ActionResult Transfer([FromBody] TransferMoneyRequest transferMoneyRequest)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var accounts = tmpUser.Accounts;
            var modified = accounts.Where(acc => acc.AccountNumber == transferMoneyRequest.SourceAccountNumber).FirstOrDefault();
            var destination = _unitOfWork.Accounts.GetAll().Where(docs => docs.AccountNumber == transferMoneyRequest.DestinationAccountNumber).FirstOrDefault();
            var destinationUser = _unitOfWork.Users.Find(usr => usr.Accounts.Contains(destination)).FirstOrDefault();
            if (!((modified.Balance - transferMoneyRequest.Amount) >= 0)) return BadRequest("you cannot transfer more than you own");
            modified.Balance -= transferMoneyRequest.Amount;
            destination.Balance+= transferMoneyRequest.Amount;
            _unitOfWork.Accounts.Update(modified);
            _unitOfWork.Accounts.Update(destination);
            tmpUser.AccountHistory.Add(new History()
            {
                FromAccountNumber = transferMoneyRequest.SourceAccountNumber,
                Amount = transferMoneyRequest.Amount * -1,
                DestinationAccountNumber = transferMoneyRequest.DestinationAccountNumber,
                TransactionType = SourceType.Transfer
            });
            destinationUser.AccountHistory.Add(new History()
            {
                FromAccountNumber = transferMoneyRequest.SourceAccountNumber,
                Amount = transferMoneyRequest.Amount,
                DestinationAccountNumber = transferMoneyRequest.DestinationAccountNumber,
                TransactionType = SourceType.Transfer
            });
            _unitOfWork.Users.Update(destinationUser);
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Complete();
            return Ok();
        }







    }
}
