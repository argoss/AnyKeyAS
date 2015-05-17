using System.Threading.Tasks;
using System.Web.Http;
using Site.Models.Clients;

namespace Site.Controllers.Clients
{
    public class ClientApiController: ApiController
    {
        [HttpGet]
        public async Task<ClientListViewModel> GetClients()
        {

            return null;
        }

        [HttpPost]
        public async Task AddClient()
        {

            
        }
    }
}