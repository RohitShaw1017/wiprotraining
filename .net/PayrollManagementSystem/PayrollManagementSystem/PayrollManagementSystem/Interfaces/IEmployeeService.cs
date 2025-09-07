using PayrollManagementSystem.Models;
using System.Collections.Generic;

namespace PayrollManagementSystem.Interfaces
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employeeData);
        void UpdateEmployee(Employee employeeData);
        void RemoveEmployee(int employeeId);
    }
}
