using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicing.Clients
{
    public interface IClientService
    {
        Task<ClientModel[]> GetClients();

        Task<ClientModel> GetClient(int id);

        Task SaveClient(ClientModel model);

        Task DeleteClient(int id);
    }
}
