using Dapper;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class DeleteManager
{
    public async Task<int> DeleteByIdAsync(int id)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = """
                           delete from Employees
                           where EmployeeID = @id
                           """;
        
        var result = await connection.ExecuteAsync(sql, new { id });
        return result;
    }
    
    public async Task<int> DeleteByIdGroup(IEnumerable<int> ids)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = """
                            delete from Employees
                            where EmployeeID in @ids
                            """;
        
        var result = await connection.ExecuteAsync(sql, new { ids });
        return result;
    }
}