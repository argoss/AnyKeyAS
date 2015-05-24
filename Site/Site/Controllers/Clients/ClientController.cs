using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Servicing.Clients;
using Site.Models.Clients;

namespace Site.Controllers.Clients
{
    public class ClientController: Controller
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService ?? new ClientService();
        }

        public ActionResult Clients()
        {
            var model = _clientService.GetClients().Result;

            return PartialView(new ClientListViewModel { List = Mapper.Map<ClientModel[], ClientViewModel[]>(model) });
        }
    }
}