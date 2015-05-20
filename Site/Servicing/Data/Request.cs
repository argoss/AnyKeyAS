using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Servicing.Requests;

namespace Servicing.Data
{
    [Table("Request")]
    public class Request
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        public RequestStatus Status { get; set; }

        [ForeignKey("Client"), Column(Order = 1)]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
