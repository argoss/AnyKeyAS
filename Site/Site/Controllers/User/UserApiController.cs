using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Account;
using Servicing.Clients;
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

        public UserApiController(IAccountService accountService)
        {
            _accountService = accountService ?? new AccountService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Passwords mismatch");
            }
            var asResult = await _accountService.CreateUser(model.UserName, model.Password).ConfigureAwait(false);
            if (!asResult.IsSuccess)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);

            bool result = true;//await ModifyUser(model).ConfigureAwait(false);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, result);
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

        /*[HttpGet]
        public async Task GetUser(string name)
        {
            await _accountService.GetUser(name);
        }*/

        [HttpDelete]
        public async Task DeleteUser(string name)
        {
            await _accountService.DeleteUser(name);
        }
    }
}