using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollManagementSystem.Models
{
    public class Tax
    {
        public int TaxID { get; set; }
        public int EmployeeID { get; set; }
        public int TaxYear { get; set; }

        [Column(TypeName = "decimal(18,2)")] public decimal TaxableIncome { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal TaxAmount { get; set; }

        public Employee? Employee { get; set; }
    }
}
