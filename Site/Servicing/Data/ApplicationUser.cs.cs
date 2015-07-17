using Microsoft.AspNet.Identity.EntityFramework;
using Servicing.Account;

namespace Servicing.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public Position Position { get; set; }
    }
}
