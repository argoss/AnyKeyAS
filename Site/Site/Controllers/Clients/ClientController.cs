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

        public ActionResult ClientEdit(int? id = null)
        {
            var model = id == null ? new ClientViewModel() : 
                Mapper.Map<ClientModel, ClientViewModel>(_clientService.GetClient(id.Value).Result);

            return View(model);
        }
    }
}