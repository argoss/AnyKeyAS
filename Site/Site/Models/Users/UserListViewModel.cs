﻿using System;
using System.ComponentModel.DataAnnotations;
using Servicing.Account;

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

    public class BaseUserViewModel
    {
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
        public string Position { get; set; }
    }

    public class UserViewModel : BaseUserViewModel
    {
        public string Roles { get; set; }
    }

    public class UserEditViewModel : BaseUserViewModel
    {
        private UserRoles[] _roleses = new UserRoles[0];

        [Required]
        public UserRoles[] Roles
        {
            get { return _roleses; }
            set { _roleses = value ?? new UserRoles[0]; }
        }
    }

    public class UserCreateViewModel : UserEditViewModel
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