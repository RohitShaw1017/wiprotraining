using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollManagementSystem.Models
{
    public class Tax
    {

        [Key]
        public int TaxID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        public int TaxYear { get; set; }

        [Required]
        public decimal TaxableIncome { get; set; }

        [Required]
        public decimal TaxAmount { get; set; }

        public Employee Employee { get; set; }

    }
}
