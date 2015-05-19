using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicing.Requests
{
    public interface IRequestService
    {
        Task<RequestModel[]> GetRequests();

        Task<RequestModel> GetRequest(int id);

        Task SaveRequest(RequestModel model);

        Task DeleteRequest(int id);
    }
}
