using System;
using System.ComponentModel.DataAnnotations;
using Servicing.Data;
using Servicing.Requests;
using Site.Models.Clients;

namespace Site.Models.Requests
{
    public class RequestListViewModel
    {
        public RequestListViewModel()
        {
            List = new RequestViewModel[0];
        }

        public RequestViewModel[] List { get; set; }
    }

    public class RequestViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [Required]
        public string Status { get; set; }

        public ClientViewModel Client { get; set; }
    }
}