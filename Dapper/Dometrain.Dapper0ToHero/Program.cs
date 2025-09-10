using Dometrain.Dapper0ToHero;
using Dometrain.Dapper0ToHero.Entities;
using Dometrain.Dapper0ToHero.Managers;
using Z.Dapper.Plus;

await DatabaseInitializer.InitializeAsync();

var relationshipManager = new RelationshipManager();
// var department = await relationshipManager.GetDepartmentWithEmployees(1);
//
// Console.WriteLine($"Department: {department?.Name}");
// Console.WriteLine($"Employees: {department?.Employees.Count}");

DapperPlusManager.Entity<Employee>().Table("Employees").Identity(x => x.EmployeeID)
    .UseBulkOptions(x => x.IgnoreOnMergeUpdateExpression = employee => new {employee.LastName}); // change the first name but ignore last name change
var bulkManager = new BulkManager();
// var bulkEmployees = new List<Employee>()
// {
//     new Employee()
//     {
//         FirstName = "Miley",
//         LastName = "Cyrus",
//         DepartmentID = 1,
//         Email = "m.circus@mail.ru",
//         HireDate = "2020-08-10"
//     },
//     new Employee()
//     {
//         FirstName = "John",
//         LastName = "Doe",
//         DepartmentID = 2,
//         Email = "XXXXXXXXXXXXXXXX",
//         HireDate = "2020-08-10"
//     },
//     new Employee()
//     {
//         FirstName = "Jane",
//         LastName = "Doe",
//         DepartmentID = 3,
//         Email = "XXXXXXXXXXXXXXXX",
//         HireDate = "2020-08-10"
//     }
// };

// await bulkManager.BulkInsertAsync(employees: bulkEmployees);

var queryManager = new QueryManager();
// var employees = await queryManager.GetEmployeesByDepartmentId(1);
// foreach (var employee in employees)
// {
//     employee.FirstName += " updated";
// }
// await bulkManager.BulkUpdateAsync(employees);

// var upsertList = new List<Employee>()
// {
//     new Employee()
//     {
//         FirstName = "Miley",
//         LastName = "Cyrus",
//         DepartmentID = 1,
//         Email = "XXXXXXXXXXXXXXXX",
//         HireDate = "2020-08-10",
//         EmployeeID = 1
//     },
//     new Employee()
//     {
//         FirstName = "Darron",
//         LastName = "Aranowski",
//         DepartmentID = 2,
//         Email = "d.arno@toon.expo",
//         HireDate = "1915-08-10",
//         EmployeeID = 2
//     },
//     new Employee()
//     {
//         FirstName = "Another",
//         LastName = "One inserted",
//         DepartmentID = 3,
//         Email = "inserted@another.one",
//         HireDate = "2020-08-10"
//     }
// };
//
// await bulkManager.BulkMergeAsync(upsertList);

// var deleteList = new List<Employee>()
// {
//     new Employee()
//     {
//         EmployeeID = 1
//     },
//     new Employee()
//     {
//         FirstName = "Darron",
//         LastName = "Aranowski",
//         DepartmentID = 2,
//         Email = "d.arno@toon.expo",
//         HireDate = "1915-08-10",
//         EmployeeID = 13
//     }
// };
//
// await bulkManager.BulkDeleteAsync(deleteList);

// Temporary table example
var tempTableManager = new TempTableManager();
var highPerformers = await tempTableManager.GetHighPerformingEmployees();
foreach (var emp in highPerformers)
{
    Console.WriteLine($"{emp.FirstName} {emp.LastName} - Dept: {emp.DepartmentID}");
}