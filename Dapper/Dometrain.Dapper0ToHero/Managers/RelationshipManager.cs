using Dapper;
using Dometrain.Dapper0ToHero.Entities;
using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero.Managers;

public class RelationshipManager
{
    public async Task<Department?> GetDepartmentWithEmployees(int departmentId)
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();
        const string sql = """
                           SELECT d.DepartmentID, e.EmployeeID, e.FirstName, e.LastName FROM Employees e
                            join Departments d on e.DepartmentID = d.DepartmentID
                            where d.DepartmentID = @departmentId
                           """;
        var departments = await connection.QueryAsync<Department, Employee, Department>(sql,
            map: (department, employee) =>
            {
                department.Employees.Add(employee);
                return department;
            },
            splitOn: "EmployeeID",
            param: new { departmentId });
        return departments.FirstOrDefault();
    }
}