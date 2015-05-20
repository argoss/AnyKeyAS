using System;
using System.ComponentModel.DataAnnotations;

namespace Servicing.Requests
{
    public class RequestModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        public RequestStatus Status { get; set; }

        public int ClientId { get; set; }
    }
}
