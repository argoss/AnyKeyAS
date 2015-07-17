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
using Site.Common;
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
        [AjaxAuthorize(Role.Admin)]
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

        [AjaxAuthorize(Role.Admin)]
        public async Task<UserRoles[]> GetRoleList()
        {
            var list = _roleService.List().Select(x => new UserRoles
            {
                Flag = false,
                Name = x.Name
            }).ToArray();
            return list;
        }

        [HttpGet]
        [AjaxAuthorize(Role.Admin)]
        public async Task<UserListViewModel> GetUsers()
        {
            var items = await _accountService.GetUsers().ConfigureAwait(false);

            return new UserListViewModel { List = Mapper.Map<UserEditModel[], UserViewModel[]>(items) };
        }

        [HttpGet]
        [AjaxAuthorize(Role.Admin)]
        public async Task<UserEditViewModel> GetUser(Guid id)
        {
            var item = await _accountService.GetUserById(id.ToString());

            var model = Mapper.Map<UserEditModel, UserEditViewModel>(item);

            var roles = _roleService.List();
            model.Roles = roles.Select(x => new UserRoles
            {
                Flag = item.Roles.Contains(x.Name),
                Name = x.Name
            }).ToArray();

            return model;
        }

        [HttpPost]
        [AjaxAuthorize(Role.Admin)]
        public async Task<HttpResponseMessage> Edit(UserEditViewModel model)
        {
            var result = await _accountService.ModifyUser(Mapper.Map<UserEditViewModel, UserEditModel>(model)).ConfigureAwait(false);
            
            return Request.CreateResponse(result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);
        }

        [HttpDelete]
        [AjaxAuthorize(Role.Admin)]
        public async Task DeleteUser(string id)
        {
            await _accountService.DeleteUser(id);
        }
    }
}