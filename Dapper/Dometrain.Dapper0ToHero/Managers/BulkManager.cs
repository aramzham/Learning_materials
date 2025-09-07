using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;
using Z.Dapper.Plus;

namespace Dometrain.Dapper0ToHero.Managers;

public class BulkManager
{
    public async Task BulkInsertAsync(IEnumerable<Employee> employees)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        await connection.BulkInsertAsync(employees);
    }
    
    public async Task BulkUpdateAsync(IEnumerable<Employee> employees)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        await connection.BulkUpdateAsync(employees);
    }
    
    public async Task BulkDeleteAsync(IEnumerable<Employee> employees)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        await connection.BulkDeleteAsync(employees);
    }
    
    public async Task BulkMergeAsync(IEnumerable<Employee> employees)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        await connection.BulkMergeAsync(employees);
    }
}