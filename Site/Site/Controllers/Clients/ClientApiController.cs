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
            var model = await _clientService.GetClients().ConfigureAwait(false);

            return new ClientListViewModel { List = Mapper.Map<ClientModel[], ClientViewModel[]>(model) };
        }

        [HttpGet]
        public async Task<ClientViewModel> GetClient(int? id = null)
        {
            var model = id == null ? new ClientViewModel() : Mapper.Map<ClientModel, ClientViewModel>(await _clientService.GetClient(id.Value).ConfigureAwait(false));

            return model;
        }

        [HttpPost]
        public async Task AddClient(ClientViewModel model)
        {
            await _clientService.SaveClient(Mapper.Map<ClientViewModel, ClientModel>(model));
        }

        [HttpDelete]
        public async Task DeleteClient(int id)
        {
            await _clientService.DeleteClient(id).ConfigureAwait(false);
        }
    }
}