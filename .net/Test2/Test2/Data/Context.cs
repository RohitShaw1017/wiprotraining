using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using Test2.Models;

namespace Test2.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
         : base(options) { }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
    }
}
