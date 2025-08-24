using Microsoft.EntityFrameworkCore;
using Test_MVC_.Models;

namespace Test_MVC_.Data
{
    public class NewDbContext: DbContext
    {
        public NewDbContext(DbContextOptions<NewDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
