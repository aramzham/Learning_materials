namespace Dometrain.Dapper0ToHero.Entities;

public class Employee
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string? HireDate { get; set; }
    public int? DepartmentID { get; set; }

    public override string ToString()
    {
        return $"EmployeeID: {EmployeeID}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, HireDate: {HireDate}, DepartmentID: {DepartmentID}";
    }
}