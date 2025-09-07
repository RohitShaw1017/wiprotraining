using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Interfaces
{
    public interface IFinancialRecordService
    {
        void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}
