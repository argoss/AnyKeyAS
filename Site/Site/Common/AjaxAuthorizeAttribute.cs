using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Site.Common
{
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {

        public AjaxAuthorizeAttribute(params Role[] roles)
        {
            Roles = roles;
        }

        public new Role[] Roles
        {
            get
            {
                return ToRoleArray(base.Roles);
            }
            set
            {
                base.Roles = value != null ? string.Join(", ", value.Select(x => x.ToString())) : string.Empty;
            }
        }

        public Role[] ToRoleArray(string roles)
        {
            var result = new List<Role>();
            foreach (var strRole in roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrWhiteSpace(strRole)) continue;

                Role role;
                if (!Enum.TryParse(strRole.Trim(), out role))
                {
                    throw new Exception(string.Format("Unknown role {0}", strRole));
                }
                result.Add(role);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using AuthorizeAttribute. The filterContext object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new AjaxHttpUnauthorizedResult();
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private class AjaxHttpUnauthorizedResult : HttpUnauthorizedResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                base.ExecuteResult(context);

                context.HttpContext.Response.End();
            }
        }
    }
}