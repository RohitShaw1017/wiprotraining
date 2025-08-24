
using MVCExample1.Models;
using System.Collections.Generic;

namespace MVCExample1.Controllers
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> patients { get; set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}