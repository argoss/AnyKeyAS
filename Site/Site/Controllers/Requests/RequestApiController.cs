﻿using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Requests;
using Site.Common;
using Site.Models.Requests;

namespace Site.Controllers.Requests
{
    [Authorize]
    public class RequestApiController: ApiController
    {
        private IRequestService _requestService;

        public RequestApiController(IRequestService requestService)
        {
            _requestService = requestService ?? new RequestService();
        }

        [HttpGet]
        [AjaxAuthorize(Role.Operator, Role.ServiceEngineer, Role.Admin)]
        public async Task<RequestListViewModel> GetRequests()
        {
            var model = await _requestService.GetRequests();

            return new RequestListViewModel { List = Mapper.Map<RequestModel[], RequestViewModel[]>(model) };
        }

        [HttpGet]
        [AjaxAuthorize(Role.Operator, Role.ServiceEngineer, Role.Admin)]
        public async Task<RequestViewModel> GetRequest(int id)
        {
            var model = await _requestService.GetRequest(id);

            return Mapper.Map<RequestModel, RequestViewModel>(model);
        }

        [HttpPost]
        [AjaxAuthorize(Role.Operator, Role.Admin)]
        public async Task AddRequest(RequestViewModel model)
        {
            await _requestService.SaveRequest(Mapper.Map<RequestViewModel, RequestModel>(model));
        }

        [HttpDelete]
        [AjaxAuthorize(Role.Operator, Role.Admin)]
        public async Task DeleteRequest(int id)
        {
            await _requestService.DeleteRequest(id);
        }
    }
}