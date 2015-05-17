using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Servicing.Account;
using Site.Models;

namespace Site.Controllers.User
{
    [Authorize(Roles = "admin")]
    public sealed class UserApiController : ApiController
    {
        private IAccountService _accountService;

        public UserApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [System.Web.Http.HttpPost]
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
        public async Task<HttpResponseMessage> GetUsers()
        {

            return null;
        }
    }
}