using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servicing.Data
{
    [Table("Equipment")]
    public class Equipment
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }


    }
}
