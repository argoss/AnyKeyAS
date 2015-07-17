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

        public DateTime? CreationDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public RequestStatus Status { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
