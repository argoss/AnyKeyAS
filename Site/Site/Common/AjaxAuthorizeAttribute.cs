using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using Servicing.Account;
using Servicing.Roles;

namespace Site.Common
{
    public class AjaxAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private IAccountService _accountService;

        public AjaxAuthorizeAttribute(params Role[] roles)
        {
            Roles = roles.Select(role => role.ToString()).ToArray();

            _accountService = new AccountService(new RoleService());
        }

        public new string[] Roles
        {
            get
            {
                return base.Roles.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            set
            {
                base.Roles = value != null ? string.Join(", ", value) : string.Empty;
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var id = Thread.CurrentPrincipal.Identity.GetUserId();
            var user = _accountService.GetUserById(id).Result;

            if (user == null)
                base.HandleUnauthorizedRequest(actionContext);

            if (!Roles.Any(x => user.Roles.Contains(x, new RoleComparer())))
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            
        }
    }

    public class RoleComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.ToLower() == y.ToLower();
        }

        public int GetHashCode(string x)
        {
            if (Object.ReferenceEquals(x, null)) return 0;
                return 1;
        }
    }


    /*public class AjaxAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
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
    }*/
}