﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Servicing.Data;

namespace Servicing.Clients
{
    public class ClientService : IClientService
    {
        public async Task<ClientModel[]> GetClients()
        {
            using (var dc = new AnykeyDbCntext())
            {
                var items = await dc.Clients.ToArrayAsync().ConfigureAwait(false);

                return items.Select(FromDb).ToArray();
            }
        }

        public async Task<ClientModel> GetClient(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Clients.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

                return FromDb(item);
            }
        }

        public async Task SaveClient(ClientModel model)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Clients.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
                if (item == null)
                {
                    item = new Client();
                    dc.Clients.Add(item);
                }

                item.Name = model.Name;
                item.OwnPhone = model.OwnPhone;
                item.WorkPhone = model.WorkPhone;
                item.ShortName = model.ShortName;
                item.Addres = model.Addres;

                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteClient(int id)
        {
            using (var dc = new AnykeyDbCntext())
            {
                var item = await dc.Clients.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                if (item == null)
                {
                    return;
                }

                dc.Clients.Remove(item);
                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private Client ToDb(ClientModel model)
        {
            return new Client
            {
                Id = model.Id,
                Addres = model.Addres,
                OwnPhone = model.OwnPhone,
                WorkPhone = model.WorkPhone,
                ShortName = model.ShortName
            };
        }

        private ClientModel FromDb(Client item)
        {
            return new ClientModel
            {
                Id = item.Id,
                Addres = item.Addres,
                Name = item.Name,
                OwnPhone = item.OwnPhone,
                WorkPhone = item.WorkPhone,
                ShortName = item.ShortName
            };
        }
    }
}
