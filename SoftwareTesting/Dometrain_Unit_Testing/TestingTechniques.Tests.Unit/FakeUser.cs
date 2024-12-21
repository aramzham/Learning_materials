namespace TestingTechniques.Tests.Unit;

public class FakeUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public FakeAddress Address { get; set; }
}