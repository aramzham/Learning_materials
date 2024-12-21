using Xunit.Abstractions;

namespace CalculatorLib.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _output;
    private readonly Guid _guid = Guid.NewGuid();

    public CalculatorTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Theory] // if you skip here all 3 tests will be ignored
    [InlineData(1, 2, 7, Skip = "Breaks in CI")]
    [InlineData(2, 4, 6)]
    [InlineData(3, 6, 9)]
    public void Add_ShouldAdd_WhenTwoNumbers(int a, int b, int expected)
    {
        // Arrange
        _output.WriteLine(_guid.ToString());

        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void AddTwoNumbersShouldEqualTheirEqual()
    {
        // Arrange
        _output.WriteLine(_guid.ToString());

        // Act
        var result = _sut.Add(5, 8);

        // Assert
        Assert.Equal(13, result);
    }
    
    [Fact]
    public void AddTwoNumbersShouldNotEqualTheirEqual()
    {
        // Arrange
        _output.WriteLine(_guid.ToString());

        // Act
        var result = _sut.Add(5, 8);

        // Assert
        Assert.NotEqual(1, result);
    }
    
    [Fact(Skip = "not completed test")]
    public void Add_ShouldDoSomething_WhenSomethingHappens()
    {
        // Arrange
        _output.WriteLine(_guid.ToString());

        // Act
        var result = _sut.Add(5, 8);

        // Assert
        Assert.NotEqual(1, result);
    }
}