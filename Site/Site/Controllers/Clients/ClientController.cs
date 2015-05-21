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

        public async Task<ActionResult> Clients()
        {
            var model = await _clientService.GetClients().ConfigureAwait(false);

            return View(new ClientListViewModel { List = Mapper.Map<ClientModel[], ClientViewModel[]>(model) });
        }
    }
}