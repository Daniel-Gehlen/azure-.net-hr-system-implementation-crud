// EmployeeLogService.cs (Service)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeLogService
{
    private readonly EmployeeLogTableService _employeeLogTableService;

    public EmployeeLogService(EmployeeLogTableService employeeLogTableService)
    {
        _employeeLogTableService = employeeLogTableService;
    }

    public async Task InsertLogAsync(EmployeeLog log)
    {
        await _employeeLogTableService.InsertLogAsync(log);
    }

    public async Task<EmployeeLog> RetrieveLogAsync(int employeeId, DateTime modificationDate)
    {
        return await _employeeLogTableService.RetrieveLogAsync(employeeId, modificationDate);
    }

    public async Task<List<EmployeeLog>> RetrieveAllLogsAsync(int employeeId)
    {
        return await _employeeLogTableService.RetrieveAllLogsAsync(employeeId);
    }

    public async Task UpdateLogAsync(EmployeeLog log)
    {
        await _employeeLogTableService.UpdateLogAsync(log);
    }

    public async Task DeleteLogAsync(EmployeeLog log)
    {
        await _employeeLogTableService.DeleteLogAsync(log);
    }
}
