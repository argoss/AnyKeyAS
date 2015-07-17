using System;

namespace Site.Models.Service
{
    public class ServiceListViewModel
    {
        public ServiceListViewModel()
        {
            List = new ServiceViewModel[0];
        }

        public ServiceViewModel[] List { get; set; }
    }

    public class ServiceViewModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Status { get; set; }

        public int ClientId { get; set; }
    }
}