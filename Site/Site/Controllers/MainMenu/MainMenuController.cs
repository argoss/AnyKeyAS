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
                ClientsUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ClientApi"}),
                RequestUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "RequestApi"}),
                ServicingUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ServicingApi"}),
                UserUrl = Url.HttpRouteUrl("DefaultApi", new { controller = "UserApi" })
            };

            return View(url);
        }
    }
}