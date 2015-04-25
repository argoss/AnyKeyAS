using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Servicing.Account
{
    public interface IAccountService : IDisposable
    {
        Task<bool> ValidateUser(string userName, string password);

        Task<AccountServiceResult> CreateUser(string userName, string password, string email);

        Task<AccountServiceResult> ModifyUser(UserEditModel model);
        
        Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);

        Task<AccountServiceResult> ResetPassword(string userName, string token, string newPassword);

        Task<bool> DeleteUser(string userName);

        Task<AccountServiceResult> ValidatePassword(string password);

        Task<AccountServiceResult> AddToRole(string userName, string roleName);

        Task<AccountServiceResult> RemoveFromRole(string userName, string roleName);

        Task<bool> SignInAsync(string userName, bool isPersistent, RequestContext requestContext);

        void SignOut(RequestContext requestContext);

        Task<string> GeneratePasswordResetTokenAsync(string userName);

        Task SendEmailAsync(string userName, string subject, string body);

        Task<UserEditModel> GetUser(string userName);

        Task<UserEditModel> GetUserById(string userId);
    }
}
