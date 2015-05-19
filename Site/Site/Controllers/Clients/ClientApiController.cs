using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Servicing.Clients;
using Site.Models.Clients;

namespace Site.Controllers.Clients
{
    public class ClientApiController: ApiController
    {
        private IClientService _clientService;

        public ClientApiController(IClientService clientService)
        {
            _clientService = clientService ?? new ClientService();
        }

        [HttpGet]
        public async Task<ClientListViewModel> GetClients()
        {
            var model = await _clientService.GetClients();

            return new ClientListViewModel { List = Mapper.Map<ClientModel[], ClientViewModel[]>(model) };
        }

        [HttpPost]
        public async Task AddClient(ClientViewModel model)
        {
            await _clientService.SaveClient(Mapper.Map<ClientViewModel, ClientModel>(model));
        }

        [HttpDelete]
        public async Task DeleteClient(int id)
        {
            await _clientService.DeleteClient(id);
        }
    }
}