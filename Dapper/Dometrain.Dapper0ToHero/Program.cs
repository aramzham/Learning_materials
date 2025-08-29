// See https://aka.ms/new-console-template for more information

using Dometrain.Dapper0ToHero.Entities;
using Dometrain.Dapper0ToHero.Managers;

Console.WriteLine("Hello, World!");

var insertManager = new InsertManager();
// var id = await insertManager.InsertEmployeeAsync(new Employee()
// {
//     FirstName = "John",
//     LastName = "Doe",
//     Email = "john.doe@example.com",
//     HireDate = "2020-09-19",
//     DepartmentID = 10
// });
// Console.WriteLine($"inserted id = {id}");

// List<Employee> employees =
// [
//     new()
//     {
//         FirstName = "Johnny",
//         LastName = "Walker",
//         Email = "johnny.walker@example.com",
//         HireDate = "2020-09-20",
//         DepartmentID = 9
//     },
//     new()
//     {
//         FirstName = "Karen",
//         LastName = "Abazyan",
//         Email = "abaz@example.com",
//         HireDate = "2020-09-21",
//         DepartmentID = 8
//     }
// ];
// var rows = await insertManager.InsertEmployeesAsync(employees);
// Console.WriteLine($"inserted row count = {rows}");

var queryManager = new QueryManager();
// var departmentsCount = await queryManager.DepartmentsCount();
// Console.WriteLine($"departments count = {departmentsCount}");
//
// var lastName = await queryManager.GetEmployeeLastName(3);
// Console.WriteLine($"employee last name = {lastName}");
//
// var employee = await queryManager.GetEmployeeById(4);
// Console.WriteLine($"employee = {employee}");
//
// var operationsEmployees = await queryManager.GetEmployeesByDepartmentId(9);
// Console.WriteLine($"in operations department we have:{Environment.NewLine}");
// foreach (var e in operationsEmployees)
// {
//     Console.WriteLine($"employee = {e}");
// }

var deleteManager = new DeleteManager();
// var deletedRow = await deleteManager.DeleteByIdAsync(12);
// Console.WriteLine($"deleted row count = {deletedRow}");
//
// var deletedRows = await deleteManager.DeleteByIdGroup([11,12,13]);
// Console.WriteLine($"deleted row count = {deletedRows}");

var employeesByDepartmentId = await queryManager.GetEmployeesByDepartmentId(9);
Console.WriteLine($"in operations department we have:{Environment.NewLine}");
foreach (var e in employeesByDepartmentId)
{
    Console.WriteLine($"employee = {e.EmployeeID} {e.FirstName} {e.Email}");
}