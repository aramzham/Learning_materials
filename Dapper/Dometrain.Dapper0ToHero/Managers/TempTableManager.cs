using Dapper;
using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class TempTableManager
{
    public async Task<IEnumerable<Employee>> GetHighPerformingEmployees()
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();

        const string sql = """
            -- Create temporary table for high-performing employees
            CREATE TEMP TABLE temp_high_performers AS
            SELECT e.*, d.Name as DepartmentName
            FROM Employees e
            JOIN Departments d ON e.DepartmentID = d.DepartmentID
            WHERE d.Budget > 300000;

            -- Query from temp table
            SELECT EmployeeID, FirstName, LastName, Email, HireDate, DepartmentID
            FROM temp_high_performers
            ORDER BY FirstName;
            """;

        return await connection.QueryAsync<Employee>(sql);
    }
}