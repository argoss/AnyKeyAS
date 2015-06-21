using System.Web.Mvc;
using Site.Models.MainMenu;

namespace Site.Controllers.MainMenu
{
    public class MainMenuController: Controller
    {
        public ActionResult Index()
        {
            var url = new MainMenuApiConfig
            {
                //ClientsUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ClientApi"}),
                ClientsUrl = Url.HttpRouteUrl("DefaultActionApi", new { controller = "ClientApi", action = "GetClients" }),
                RequestUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "RequestApi"}),
                ServicingUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ServicingApi"}),
                UserUrl = Url.HttpRouteUrl("DefaultActionApi", new { controller = "UserApi", action = "GetUsers" })
            };
            // UserUrl = Url.HttpRouteUrl("DefaultApi", new { controller = "UserApi" })

            return View(url);
        }
    }
}