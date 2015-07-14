using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Account;
using Servicing.Clients;
using Servicing.Roles;
using Servicing.Users;
using Site.Models;
using Site.Models.Clients;
using Site.Models.Users;

namespace Site.Controllers.User
{
    [Authorize(Roles = "admin")]
    public sealed class UserApiController : ApiController
    {
        private IAccountService _accountService;
        private IRoleService _roleService;

        public UserApiController(IAccountService accountService, IRoleService roleService)
        {
            _roleService = roleService;
            _accountService = accountService ?? new AccountService(new RoleService());
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(UserCreateViewModel model)
        {
            if (model.Password != model.ConfirmPassword || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Passwords mismatch");
            }
            var result = await _accountService.CreateUser(model.UserName, model.Password).ConfigureAwait(false);
            if (!result.IsSuccess)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            result = await _accountService.ModifyUser(Mapper.Map<UserCreateViewModel, UserCreateModel>(model)).ConfigureAwait(false);

            return Request.CreateResponse(result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);
        }

        public async Task<String[]> GetRoleList()
        {
            var roles = _roleService.List();
            return roles.Select(x => x.Name).ToArray();
        }

        [HttpGet]
        public async Task<UserListViewModel> GetUsers()
        {
            var items = await _accountService.GetUsers().ConfigureAwait(false);

            return new UserListViewModel { List = Mapper.Map<UserEditModel[], UserViewModel[]>(items) };
        }

        [HttpGet]
        public async Task<UserEditViewModel> GetUser(Guid id)
        {
            var item = await _accountService.GetUserById(id.ToString());

            var roles = _roleService.List();
            var userRoleModel = roles.Select(x => new UserRoles
            {
                Flag = item.Roles.Contains(x.Name),
                Name = x.Name
            }).ToArray();

            var model = Mapper.Map<UserEditModel, UserEditViewModel>(item);
            model.Roles = userRoleModel;

            return model;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Edit(UserCreateViewModel model)
        {
            var result = await _accountService.ModifyUser(Mapper.Map<UserCreateViewModel, UserCreateModel>(model)).ConfigureAwait(false);
            
            return Request.CreateResponse(result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);
        }

        [HttpDelete]
        public async Task DeleteUser(string id)
        {
            await _accountService.DeleteUser(id);
        }
    }
}