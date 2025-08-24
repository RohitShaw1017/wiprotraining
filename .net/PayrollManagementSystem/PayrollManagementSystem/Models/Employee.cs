using System.ComponentModel.DataAnnotations;

namespace PayrollManagementSystem.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        = 0;
        [Required]
        public int EmployeeName { get; set; }
        = 0;
            
        [Required]
        public int EmployeeAge { get; set; }
        = 0;
            
            
        [Required]
        public int EmployeeStatus { get; set; }
        = 0;
            
        [Required]
        public int EmployeeType { get; set; }
        = 0;
            
            

        [Required]

       
        
    }
}
