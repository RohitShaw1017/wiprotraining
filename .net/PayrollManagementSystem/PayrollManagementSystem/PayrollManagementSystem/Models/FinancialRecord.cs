using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollManagementSystem.Models
{
    public class FinancialRecord
    {
        [Key]
        public int RecordID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(20)]
        public string RecordType { get; set; }

        public Employee Employee { get; set; }
    }
}
