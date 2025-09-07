
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollManagementSystem.Models
{
    public class Employee 
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public DateTime? TerminationDate { get; set; }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

    }
}