using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Servicing.Data;
using Servicing.Roles;

namespace Servicing.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;

        public AccountService(IRoleService roleService)
        {
            _roleService = roleService;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AnykeyDbCntext()))
            {
                PasswordHasher = new SqlPasswordHasher()
            };

            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Sample");
            _userManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
        }


        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password).ConfigureAwait(false);
            return user != null;
        }

        public async Task<AccountServiceResult> CreateUser(string userName, string password)
        {
            var user = new ApplicationUser() { UserName = userName };
            return GetResult(await _userManager.CreateAsync(user, password).ConfigureAwait(false));
        }

        public async Task<AccountServiceResult> ModifyUser(UserEditModel model)
        {
            try
			{
				var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
			    user.Patronymic = model.Patronymic;
				user.Email = model.Email;
				user.PhoneNumber = model.Phone;
			    //user.Roles = new IdentityUserRole[] {_roleService.GetByName(model.Roles)};

                await _userManager.UpdateAsync(user).ConfigureAwait(false);
                return new AccountServiceResult {IsSuccess = true};
            }
            catch (Exception exception)
            {
                return new AccountServiceResult { IsSuccess = false, Errors = new[] {exception.ToString()}};                
            }
        }

        public async Task<bool> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user.Id, oldPassword, newPassword).ConfigureAwait(false);

            return result.Succeeded;
        }

        public async Task<AccountServiceResult> ResetPassword(string userName, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null) { return new AccountServiceResult
            {
                IsSuccess = false,
                Errors = new[] { "Unknown user name" }
            }; }

            return GetResult(await _userManager.ResetPasswordAsync(user.Id, token, newPassword).ConfigureAwait(false));
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            return result.Succeeded;
        }

        public async Task<AccountServiceResult> ValidatePassword(string password)
        {
            return GetResult(await _userManager.PasswordValidator.ValidateAsync(password).ConfigureAwait(false));
        }

        public async Task<AccountServiceResult> AddToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null) { return new AccountServiceResult
            {
                IsSuccess = false,
                Errors = new[] { "Unknown user name" }
            }; }

			return GetResult(await _userManager.AddToRoleAsync(user.Id, roleName).ConfigureAwait(false));
        }

        public async Task<AccountServiceResult> RemoveFromRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null) { return new AccountServiceResult
            {
                IsSuccess = false,
                Errors = new[] { "Unknown user name" }
            }; }

            return GetResult(await _userManager.RemoveFromRoleAsync(user.Id, roleName).ConfigureAwait(false));
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
            var authenticationManager = requestContext.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            return await _userManager.GeneratePasswordResetTokenAsync(user.Id).ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string userName, string subject, string body)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            await _userManager.SendEmailAsync(user.Id, subject, body).ConfigureAwait(false);
        }

        public async Task<UserEditModel[]> GetUsers()
        {
            var users = await _userManager.Users.ToArrayAsync().ConfigureAwait(false);

            return users.Select(FromDB).ToArray();
        }

        public async Task<UserEditModel> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

            if (user == null) return null;

            return new UserEditModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Roles = (await _userManager.GetRolesAsync(user.Id).ConfigureAwait(false)).ToArray()
            };
        }

        public async Task<UserEditModel> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);

			if (user == null) return null;

			return new UserEditModel
			{
				UserName = user.UserName,
				FirstName = user.FirstName,
				LastName = user.LastName,
                Patronymic = user.Patronymic,
				Email = user.Email,
				Phone = user.PhoneNumber,
                Roles = (await _userManager.GetRolesAsync(user.Id).ConfigureAwait(false)).ToArray()
			};
        }

        private AccountServiceResult GetResult(IdentityResult identityResult)
        {
            return new AccountServiceResult
            {
                IsSuccess = identityResult.Succeeded,
                Errors = identityResult.Errors
            };
        }

        private UserEditModel FromDB(ApplicationUser item)
        {
            return new UserEditModel
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic,
                UserName = item.UserName,
                Email = item.Email,
                Phone = item.Email,
                Roles = (_userManager.GetRolesAsync(item.Id).Result).ToArray()
            };
        }

        /*private ApplicationUser ToDB()
        {
            return new ApplicationUser
            {

            };
        }*/
    }
}
