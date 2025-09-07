using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Data
{
    public class PMS : DbContext
    {
        public PMS(DbContextOptions<PMS> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<FinancialRecord> FinancialRecords { get; set; }
    }
}