using Banky.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Banky.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public HistoryController(IUnitOfWork unitOfWork, IConfiguration configuration) => _unitOfWork = unitOfWork;
        


        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var docs = tmpUser.AccountHistory;
            return Ok(docs);
        }

    }
}
