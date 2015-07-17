using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Servicing.Requests;

namespace Servicing.Data
{
    [Table("Request")]
    public class Request
    {
       public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        public RequestStatus Status { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
