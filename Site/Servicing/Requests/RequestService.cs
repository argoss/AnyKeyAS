using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Servicing.Data;
using Servicing.Extensions;

namespace Servicing.Requests
{
    public class RequestService : IRequestService
    {
        public async Task<RequestModel[]> GetRequests()
        {
            using (var dc = new AnykeyDbCntext())
            {
                var items = await dc.Requests.ToArrayAsync().ConfigureAwait(false);

                return items.Select(FromDb).ToArray();
            }
        }

        public async Task<RequestModel> GetRequest(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                if (item == null)
                    throw new ArgumentException("Unknown request id");

                return FromDb(item);
            }
        }

        public async Task SaveRequest(RequestModel model)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var client = await dc.Clients.FirstOrDefaultAsync(x => x.Id == model.ClientId).ConfigureAwait(false);
                if (client == null)
                {
                    throw new ArgumentException("Unknown client id");
                }
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
                if (item == null)
                {
                    item = ToDb(model);
                    item.Client = client;
                    dc.Requests.Add(item);
                }
                else
                {
                    item.CreationDate = model.CreationDate;
                    item.ExecutionDate = model.ExecutionDate;
                    item.ModifyDate = model.ModifyDate;
                    item.Status = model.Status;
                    item.Client = client;
                }
                
                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task ChangeStatusRequest(int id, RequestStatus status)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id ==id).ConfigureAwait(false);
                if (item == null)
                    return;

                item.Status = status;
                item.ModifyDate = DateTime.Now;

                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteRequest(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Requests.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                if (item == null)
                {
                    return;
                }

                dc.Requests.Remove(item);
                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private Request ToDb(RequestModel model)
        {
            return new Request
            {
                //Id = model.Id,
                CreationDate = model.CreationDate,
                ExecutionDate = model.ExecutionDate,
                ModifyDate = model.ModifyDate,
                Status = model.Status,
            };
        }

        private RequestModel FromDb(Request item)
        {
            return new RequestModel
            {
                Id = item.Id,
                CreationDate = item.CreationDate,
                ExecutionDate = item.ExecutionDate,
                ModifyDate = item.ModifyDate,
                Status = item.Status,
                ClientId = item.Client.Id,
                ClientName = item.Client.Name
            };
        }
    }
}
