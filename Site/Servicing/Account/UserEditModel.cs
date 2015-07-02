using System;

namespace Servicing.Account
{
    public class UserEditModel
    {
        private string[] _roleses = new string[0];

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string[] Roles
        {
            get { return _roleses; }
            set { _roleses = value ?? new string[0]; }
        }
    }

    public class UserCreateModel : UserEditModel
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
