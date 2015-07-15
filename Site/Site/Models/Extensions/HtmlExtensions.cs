using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Site.Models.Extensions
{
    public static class HtmlExtensions
    {
        /*public static bool IsSelected(this HtmlHelper html, [AspMvcController] params string[] controllers)
        {
            return controllers.Any(html.IsSelected);
        }

        public static bool IsSelected(this HtmlHelper html, [AspMvcController] string controller)
        {
            return string.Equals(controller, html.ViewContext.RouteData.GetRequiredString("controller"), StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsSelectedAction(this HtmlHelper html, [AspMvcController] string controller, [AspMvcAction] string action)
        {
            return string.Equals(controller, html.ViewContext.RouteData.GetRequiredString("controller"), StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(action, html.ViewContext.RouteData.GetRequiredString("action"), StringComparison.OrdinalIgnoreCase);
        }*/

        public static MvcHtmlString NgPartial(this HtmlHelper html, string partialViewName, string ngId)
        {
            var tag = new TagBuilder("script");
            tag.MergeAttribute("id", ngId);
            tag.MergeAttribute("type", "text/ng-template");
            tag.InnerHtml = html.Partial(partialViewName).ToString();
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString NgAction(this HtmlHelper html, string action, string controller, string ngId)
        {
            var tag = new TagBuilder("script");
            tag.MergeAttribute("id", ngId);
            tag.MergeAttribute("type", "text/ng-template");
            tag.InnerHtml = html.Action(action, controller).ToString();
            return new MvcHtmlString(tag.ToString());
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DescriptionAttribute>()
                            .Description;
        }
    }
}