using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Servicing.Account;
using Servicing.Roles;
using Site.Common;
using Site.Models.MainMenu;

namespace Site.Controllers.MainMenu
{
    [Authorize]
    public class MainMenuController: Controller
    {
        private IAccountService _accountService;

        public MainMenuController(IAccountService accountService)
        {
            _accountService = accountService ?? new AccountService(new RoleService());
        }

        public async Task<ActionResult> Index()
        {
            /*var user = await _accountService.GetUserById(User.Identity.GetUserId()).ConfigureAwait(false);

            var views = new List<ViewPermissions>();
            foreach (ViewItems item in Enum.GetValues(typeof(ViewItems)))
            {
                views.Add(new ViewPermissions
                {
                    View = item,
                    Permissions = GetViewPermissions(item, user)//(ViewItems i, UserEditModel u) => { return }
                });
            }*/

            var url = new MainMenuApiConfig
            {
                ClientsUrl = Url.HttpRouteUrl("DefaultActionApi", new { controller = "ClientApi", action = "GetClients" }),
                RequestUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "RequestApi"}),
                ServicingUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ServicingApi"}),
                UserUrl = Url.HttpRouteUrl("DefaultActionApi", new { controller = "UserApi", action = "GetUsers" }),
                //Views = views.ToArray()
            };

            return View(url);
        }

        public Permissions GetViewPermissions(ViewItems item, UserEditModel user)
        {
            var result = Permissions.Non;
            foreach (var role in user.Roles)
            {
                switch ((Role)Enum.Parse(typeof(Role), role))
                {
                    case Role.Admin:
                        result = result | Permissions.FullControl;
                        break;
                    case Role.ExitEngineer:
                        result = result | Permissions.Edit;
                        break;
                    default:
                        result = result | Permissions.Non;
                        break;
                }  
            }

            return result;
        }
    }

}