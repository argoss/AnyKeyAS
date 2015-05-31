using System;
using System.ComponentModel.DataAnnotations;
using Servicing.Requests;

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
        public RequestStatus Status { get; set; }

        public string ClientName { get; set; }
    }
}