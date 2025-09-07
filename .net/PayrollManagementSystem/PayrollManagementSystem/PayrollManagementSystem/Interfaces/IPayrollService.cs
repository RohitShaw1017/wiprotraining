using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Interfaces
{
    public interface IPayrollService
    {
        Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
