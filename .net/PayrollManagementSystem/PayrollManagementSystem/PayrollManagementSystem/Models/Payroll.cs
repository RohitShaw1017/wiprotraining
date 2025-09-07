using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollManagementSystem.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime PayPeriodStartDate { get; set; }

        [Required]
        public DateTime PayPeriodEndDate { get; set; }

        [Required]
        public decimal BasicSalary { get; set; }

        public decimal OvertimePay { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetSalary { get; set; }

        public Employee Employee { get; set; }

    }
}
