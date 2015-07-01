using Microsoft.AspNet.Identity.EntityFramework;

namespace Servicing.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }
    }
}
