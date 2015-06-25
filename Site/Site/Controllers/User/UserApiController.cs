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
            _accountService = accountService ?? new AccountService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(UserCreateViewModel model)
        {
            if (model.Password != model.ConfirmPassword || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Passwords mismatch");
            }
            var result = await _accountService.CreateUser(model.UserName, model.Password).ConfigureAwait(false);

            return Request.CreateResponse(result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);

            /*bool result = await _accountService.ModifyUser(model).ConfigureAwait(false);*/
        }

        public async Task<UserCreateViewModel> GetCreateModel()
        {
            var roles = _roleService.List();
            return new UserCreateViewModel
            {
                Roles = roles.Select(x => x.Name).ToArray()
            };
        }

        /*private async Task<bool> ModifyUser(UserItemModifyViewModel viewModel)
        {
            var result = await _accountService.ModifyUser(new UserEditModel
            {
                UserName = viewModel.UserName,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                BrokerCode = viewModel.BrokerCode,
                Company = viewModel.Company,
                CompanyCode = viewModel.CompanyCode,
                CompanyNumber = viewModel.CompanyNumber
            }).ConfigureAwait(false);

            if (!result.IsSuccess) return false;

            var currentUser = await _accountService.GetUser(viewModel.UserName).ConfigureAwait(false);

            foreach (var currentRole in currentUser.Roles.Where(x => !viewModel.Roles.Contains(x)))
            {
                result = await _accountService.RemoveFromRole(viewModel.UserName, currentRole).ConfigureAwait(false);
                if (!result.IsSuccess) return false;
            }

            foreach (var currentRole in viewModel.Roles.Where(x => !currentUser.Roles.Contains(x)))
            {
                result = await _accountService.AddToRole(viewModel.UserName, currentRole).ConfigureAwait(false);
                if (!result.IsSuccess) return false;
            }

            return result.IsSuccess;
        }*/

        [HttpGet]
        public async Task<UserListViewModel> GetUsers()
        {
            var items = await _accountService.GetUsers().ConfigureAwait(false);
            return new UserListViewModel { List = Mapper.Map<UserEditModel[], UserViewModel[]>(items) };
        }

        [HttpGet]
        public async Task<UserViewModel> GetUser(int? id = null)
        {
            var model = id == null ? new UserViewModel() : Mapper.Map<UserEditModel, UserViewModel>(await _accountService.GetUser(id.Value));

            return model;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Edit(UserCreateViewModel model)
        {
            var result = await _accountService.ModifyUser(Mapper.Map<UserCreateViewModel, UserCreateModel>(model)).ConfigureAwait(false);
            
            return Request.CreateResponse(result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);
        }

        [HttpDelete]
        public async Task DeleteUser(string name)
        {
            await _accountService.DeleteUser(name);
        }
    }
}