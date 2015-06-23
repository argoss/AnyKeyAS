using System.ComponentModel.DataAnnotations;

namespace Site.Models.Clients
{
    public class ClientListViewModel
    {
        public ClientListViewModel()
        {
            List = new ClientViewModel[0];
        }

        public ClientViewModel[] List { get; set; }
    }

    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string OwnPhone { get; set; }

        [Required]
        public string WorkPhone { get; set; }
    }
}