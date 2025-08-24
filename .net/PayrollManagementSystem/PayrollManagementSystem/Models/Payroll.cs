using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollManagementSystem.Models
{
  
        public class Payroll
        {
            public int PayrollID { get; set; }

            [Required] public int EmployeeID { get; set; }

            [DataType(DataType.Date)] public DateTime PayPeriodStartDate { get; set; }
            [DataType(DataType.Date)] public DateTime PayPeriodEndDate { get; set; }

            [Column(TypeName = "decimal(18,2)")] public decimal BasicSalary { get; set; }
            [Column(TypeName = "decimal(18,2)")] public decimal OvertimePay { get; set; }
            [Column(TypeName = "decimal(18,2)")] public decimal Deductions { get; set; }
            [Column(TypeName = "decimal(18,2)")] public decimal NetSalary { get; set; }

            // Navigation
            public Employee? Employee { get; set; }
        }
    }

