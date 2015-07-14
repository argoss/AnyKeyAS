using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Requests;
using Site.Models.Service;

namespace Site.Controllers.Service
{
    public class ServiceApiController: ApiController
    {
        private readonly IRequestService _requestService;

        public ServiceApiController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<ServiceListViewModel> List()
        {
            var items = await _requestService.GetRequests().ConfigureAwait(false);

            return new ServiceListViewModel{ List = Mapper.Map<RequestModel[], ServiceViewModel[]>(items)});
        }
    }
}