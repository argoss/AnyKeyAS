using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Requests;
using Site.Models.Extensions;
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

        [HttpGet]
        public async Task<ServiceListViewModel> List()
        {
            var items = await _requestService.GetRequests().ConfigureAwait(false);

            return new ServiceListViewModel{ List = Mapper.Map<RequestModel[], ServiceViewModel[]>(items)};
        }

        [HttpPost]
        public async Task ChangeStatus(StatusChangeModel model)
        {
            await _requestService.ChangeStatusRequest(model.Id, ToRequestStatus(model.Status));
        }

        public class StatusChangeModel
        {
            public int Id { get; set; }

            public string Status { get; set; }
        }

        private RequestStatus ToRequestStatus(string status)
        {
            switch (status)
            {
                case "Принята":
                    return RequestStatus.Accept;
                case "Доставка в СЦ":
                    return RequestStatus.DeliverySC;
                case "Доставлена в СЦ":
                    return RequestStatus.DeliveredSC;
                case "Обработана":
                    return RequestStatus.ProcessedSC;
                case "Доставка клиенту":
                    return RequestStatus.DeliveryC;
                case "Исполнена":
                    return RequestStatus.Performed;
                case "Возврат":
                    return RequestStatus.Return;
                default:
                    throw new Exception("Неизвестный статус!");
            };
        }
    }
}