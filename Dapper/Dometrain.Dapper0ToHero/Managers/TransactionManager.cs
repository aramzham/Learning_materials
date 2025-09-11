using Dapper;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class TransactionManager
{
    public async Task ExecuteTransactionAsync(int employeeId, int departmentId)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync();
        
        var deleteEmployeeSql = "DELETE FROM Employees WHERE EmployeeID = @employeeId";
        var deleteDepartmentSql = "DELETE FROM Department WHERE DepartmentID = @departmentId"; // intentional typo in the table name so dapper will throw an exception

        try
        {
            await connection.ExecuteAsync(deleteEmployeeSql, new { employeeId }, transaction);
            await connection.ExecuteAsync(deleteDepartmentSql, new { departmentId }, transaction);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}