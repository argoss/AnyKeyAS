using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using Servicing.Extensions;

namespace Site.Common
{
    public class PublicControllerConstraint : IRouteConstraint
    {
        private static List<string> _controllers;

        static PublicControllerConstraint()
        {
            _controllers = BuildManager.GetReferencedAssemblies().Cast<Assembly>().
                SelectMany(x => x.EnumerateTypes()).
                Where(x => x.IsController() && x.Namespace.StartsWith("Site.Controllers")).
                Select(x => x.Name.Replace("Controller", "").ToLower()).ToList();
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.ContainsKey("controller")) return false;

            var controller = values["controller"].ToString().ToLower();
            return _controllers.Contains(controller);
        }
    }
}