namespace ConsoleAgentWithMicrosoftAI.Services;

public class EmailService
{
    public Task EmailFriend(string friendName, string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Sending email to {friendName} with message: {message}");
        Console.ResetColor();
        return Task.CompletedTask;
    }
}