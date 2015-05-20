using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Servicing.Data;

namespace Servicing.Requests
{
    public class RequestService : IRequestService
    {
        public async Task<RequestModel[]> GetRequests()
        {
            using (var dc = new AnykeyDbCntext())
            {
                var items = await dc.Requests.ToArrayAsync();

                return items.Select(FromDb).ToArray();
            }
        }

        public async Task<RequestModel> GetRequest(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == id);
                if (item == null)
                    throw new ArgumentException("Unknown request id");

                return FromDb(item);
            }
        }

        public async Task SaveRequest(RequestModel model)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (item == null)
                {
                    item = new Request();
                    dc.Requests.Add(item);
                }

                item.CreationDate = model.CreationDate;
                item.ExecutionDate = model.ExecutionDate;

                await dc.SaveChangesAsync();
            }
        }

        public async Task DeleteRequest(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == id);
                if (item == null)
                {
                    return;
                }

                dc.Requests.Remove(item);
                await dc.SaveChangesAsync();
            }
        }

        private Request ToDb(RequestModel model)
        {
            return new Request
            {
                Id = model.Id,
                CreationDate = model.CreationDate,
                ExecutionDate = model.ExecutionDate,
                ClientId = model.ClientId
            };
        }

        private RequestModel FromDb(Request item)
        {
            return new RequestModel
            {
                Id = item.Id,
                CreationDate = item.CreationDate,
                ExecutionDate = item.ExecutionDate,
                ClientId = item.ClientId
            };
        }
    }
}
