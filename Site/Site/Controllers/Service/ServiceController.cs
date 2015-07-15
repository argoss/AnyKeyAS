using System.Web.Mvc;
using Site.Models.Service;

namespace Site.Controllers.Service
{
    public class ServiceController: Controller
    {
        public ActionResult Index()
        {
            var url = new ServiceApiConfig
            {
                RequestUrl = Url.HttpRouteUrl("DefaultApi", new {controller = "ServiceApi"})
            };
            return View(url);
        }
    }
}