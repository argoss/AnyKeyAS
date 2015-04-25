using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Servicing.Data;

namespace Servicing.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ValidateUser(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> CreateUser(string userName, string password, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> ModifyUser(UserEditModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> ResetPassword(string userName, string token, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> ValidatePassword(string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> AddToRole(string userName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountServiceResult> RemoveFromRole(string userName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SignInAsync(string userName, bool isPersistent, RequestContext requestContext)
        {
            IAuthenticationManager authenticationManager = requestContext.HttpContext.GetOwinContext().Authentication;

            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }

            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
            return true;
        }

        public void SignOut(RequestContext requestContext)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GeneratePasswordResetTokenAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task SendEmailAsync(string userName, string subject, string body)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserEditModel> GetUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserEditModel> GetUserById(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
