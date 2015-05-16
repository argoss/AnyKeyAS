using System.Web.Mvc;

namespace Site.Controllers.MainMenu
{
    public class MainMenuController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}