using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Servicing.Data
{
    public class AnykeyDbCntext : IdentityDbContext<ApplicationUser>
    {
        public AnykeyDbCntext()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
