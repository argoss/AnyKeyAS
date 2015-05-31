using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Servicing.Account;
using Site.Models.Users;

namespace Site.Controllers.User
{
    public class UserController: Controller
    {
        private IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Users()
        {
            //var model = await _accountService.GetUsers().ConfigureAwait(false);
            var url = new UserApiConfig
            {
                UserUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "UserApi"})
            };

            return View(url);
        }
    }
}