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