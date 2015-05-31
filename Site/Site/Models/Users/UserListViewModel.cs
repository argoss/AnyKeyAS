using System.ComponentModel.DataAnnotations;

namespace Site.Models.Users
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            List = new UserViewModel[0];
        }

        public UserViewModel[] List { get; set; }
    }

    public class UserViewModel
    {
        private string[] _roles = new string[0];

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string[] Roles
        {
            get { return _roles; }
            set { _roles = value ?? new string[0]; }
        }
    }
}