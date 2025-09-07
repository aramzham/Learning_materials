namespace Dometrain.Dapper0ToHero.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string? Location { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? IsActive { get; set; }

    public List<Employee> Employees { get; set; } = [];
}