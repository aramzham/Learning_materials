using FluentAssertions;

namespace TestingTechniques.Tests.Unit;

public class SampleClassTests
{
    private readonly SampleClass _sut = new();

    [Fact]
    public void User_ShouldBeAram()
    {
        // Arrange
        var fakeUser = new FakeUser()
        {
            Address = new FakeAddress(){ City = "Yerevan", Street = "Yervand Qochar" },
            Age = 34,
            Email = "my@email.am",
            Name = "Aram",
            DateOfBirth = new DateOnly(1990, 5, 26)
        };

        // Act
        var result = _sut.User;

        // Assert
        result.Should().BeEquivalentTo(fakeUser);
        // even if the types are different the matching goes prop by prop
        // the equality is checked the same way for the nested types too
    }

    [Fact]
    public void ExampleEventAssertion()
    {
        // Arrange
        var monitorSubject = _sut.Monitor();

        // Act
        _sut.RaiseEvent();

        // Assert
        monitorSubject.Should().Raise(nameof(_sut.ExampleEvent));
    }

    [Fact]
    public void InternalMemberTest()
    {
        // Arrange
        
        // Act
        var result = _sut.SecretNumber;

        // Assert
        result.Should().Be(1234);
    }
}