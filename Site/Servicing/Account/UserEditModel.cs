namespace Servicing.Account
{
    public class UserEditModel
    {
        private string[] _roles = new string[0];

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string[] Roles
        {
            get { return _roles; }
            set { _roles = value ?? new string[0]; }
        }
    }
}
