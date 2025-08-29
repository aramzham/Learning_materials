using Dapper;
using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class QueryManager
{
    public async Task<int> DepartmentsCount()
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = "SELECT COUNT(*) FROM Departments";
        var count = await connection.ExecuteScalarAsync<int>(sql);
        return count;
    }
    
    public async Task<string?> GetEmployeeLastName(int id)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = "SELECT LastName FROM Employees where EmployeeID = @employeeId";
        var lastName = await connection.ExecuteScalarAsync<string>(sql, new { employeeId = id});
        return lastName;
    }
    
    public async Task<Employee?> GetEmployeeById(int id)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = "SELECT * FROM Employees where EmployeeID = @employeeId";
        var lastName = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { employeeId = id});
        return lastName;
    }
    
    public async Task<List<Employee>> GetEmployeesByDepartmentId(int departmentId)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = "SELECT * FROM Employees where DepartmentID = @departmentId";
        var employees = await connection.QueryAsync<Employee>(sql, new { departmentId });
        return employees.ToList();
    }
    
    public async IAsyncEnumerable<Employee> GetEmployeesByDepartmentIdUsingReader(int departmentId)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = """
                           SELECT EmployeeID, FirstName as Name, Email FROM Employees where DepartmentID = @departmentId
                           """;
        await using var reader = await connection.ExecuteReaderAsync(sql, new { departmentId });
        while (await reader.ReadAsync())
        {
            yield return new Employee
            {
                EmployeeID = reader.GetInt32(0),
                FirstName = reader.GetString(reader.GetOrdinal("Name")),
                Email = await reader.IsDBNullAsync(2) ? null : reader.GetString(2)
            };
        }
    }
}