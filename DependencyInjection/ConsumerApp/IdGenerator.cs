namespace ConsumerApp;

public class IdGenerator(IConsoleWriter consoleWriter) : IIdGenerator
{
    public Guid Id { get; } = Guid.NewGuid();
    
    public void PrintId() => consoleWriter.WriteLine(Id.ToString());
}

public interface IIdGenerator
{
    Guid Id { get; }
    void PrintId();
}