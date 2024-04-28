// EmployeeLogTableService.cs (Azure Table Storage Service)
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeLogTableService
{
    private readonly CloudTable _table;

    public EmployeeLogTableService(string connectionString)
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        _table = tableClient.GetTableReference("EmployeeLogs");
        _table.CreateIfNotExistsAsync().Wait();
    }

    public async Task InsertLogAsync(EmployeeLog log)
    {
        TableOperation insertOperation = TableOperation.Insert(log);
        await _table.ExecuteAsync(insertOperation);
    }

    public async Task<EmployeeLog> RetrieveLogAsync(int employeeId, DateTime modificationDate)
    {
        string filter = TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition(nameof(EmployeeLog.PartitionKey), QueryComparisons.Equal, employeeId.ToString()),
            TableOperators.And,
            TableQuery.GenerateFilterConditionForDate(nameof(EmployeeLog.ModificationDate), QueryComparisons.Equal, modificationDate)
        );

        TableQuery<EmployeeLog> query = new TableQuery<EmployeeLog>().Where(filter);
        return await _table.ExecuteQuerySegmentedAsync(query, null).ConfigureAwait(false);
    }

    public async Task<List<EmployeeLog>> RetrieveAllLogsAsync(int employeeId)
    {
        string filter = TableQuery.GenerateFilterCondition(nameof(EmployeeLog.PartitionKey), QueryComparisons.Equal, employeeId.ToString());

        TableQuery<EmployeeLog> query = new TableQuery<EmployeeLog>().Where(filter);
        var logs = new List<EmployeeLog>();
        TableContinuationToken token = null;

        do
        {
            var queryResult = await _table.ExecuteQuerySegmentedAsync(query, token).ConfigureAwait(false);
            logs.AddRange(queryResult.Results);
            token = queryResult.ContinuationToken;
        }
        while (token != null);

        return logs;
    }

    public async Task UpdateLogAsync(EmployeeLog log)
    {
        TableOperation replaceOperation = TableOperation.Replace(log);
        await _table.ExecuteAsync(replaceOperation);
    }

    public async Task DeleteLogAsync(EmployeeLog log)
    {
        TableOperation deleteOperation = TableOperation.Delete(log);
        await _table.ExecuteAsync(deleteOperation);
    }
}
