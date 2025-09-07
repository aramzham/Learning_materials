using Dapper;
using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class EmployeeManager
{
    public async Task<IEnumerable<Employee>> GetEmployeesByHireDate(DateTime hireDate)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        const string sql = "SELECT * FROM Employees WHERE HireDate >= @HireDate ORDER BY HireDate DESC";
        return await connection.QueryAsync<Employee>(sql, new { HireDate = hireDate });
    }
}