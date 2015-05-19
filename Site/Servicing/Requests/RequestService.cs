using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicing.Requests
{
    public class RequestService : IRequestService
    {
        public async Task<RequestModel[]> GetRequests()
        {

            return new RequestModel[0];
        }

        public async Task<RequestModel> GetRequest(int id)
        {

            return new RequestModel();
        }

        public async Task SaveRequest(RequestModel model)
        {

        }

        public async Task DeleteRequest(int id)
        {

        }
    }
}
