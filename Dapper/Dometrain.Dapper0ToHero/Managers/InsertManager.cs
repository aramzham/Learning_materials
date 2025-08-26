using Dapper;
using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class InsertManager
{
    public async Task<int> InsertEmployeeAsync(Employee employee)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = """
                           INSERT INTO Employees (FirstName, LastName, Email, HireDate, DepartmentID) 
                           VALUES (@FirstName, @LastName, @Email, @HireDate, @DepartmentID);
                            select last_insert_rowid();
                           """;
        var id = await connection.ExecuteScalarAsync<int>(sql, employee);
        return id;
    }
}