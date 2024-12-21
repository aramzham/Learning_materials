namespace TestingTechniques;

public class SampleClass
{
    internal int SecretNumber = 1234;    
    
    public User User => new User()
    {
        Address = new Address() { City = "Yerevan", Street = "Yervand Qochar" },
        Age = 34,
        Email = "my@email.am",
        Name = "Aram",
        DateOfBirth = new DateOnly(1990, 5, 26)
    };

    public event EventHandler? ExampleEvent;
    
    public void RaiseEvent() => ExampleEvent?.Invoke(this, EventArgs.Empty);
}