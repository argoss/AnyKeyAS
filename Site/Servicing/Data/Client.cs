using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servicing.Data
{
    [Table("Client")]
    public class Client
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Addres { get; set; }

        [Required]
        public string OwnPhone { get; set; }

        [Required]
        public string WorkPhone { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
