using System;
using Servicing.Requests;

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
        public string ClientName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExecutionDate { get; set; }

        public string Status { get; set; }
    }
}