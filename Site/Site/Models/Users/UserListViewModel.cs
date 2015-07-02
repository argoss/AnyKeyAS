using System;
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
        private string[] _roleses = new string[0];

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string[] Roles
        {
            get { return _roleses; }
            set { _roleses = value ?? new string[0]; }
        }
    }

    public class UserCreateViewModel : UserViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}