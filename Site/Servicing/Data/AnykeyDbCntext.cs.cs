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
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);
        }
    }
}
