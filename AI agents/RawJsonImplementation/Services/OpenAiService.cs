using System.Text.Json;
using System.Text.Json.Serialization;
using RawJsonImplementation.Models;

namespace RawJsonImplementation.Services;

public interface IOpenAiService
{
    Task<ChatMessage> GetCompletion(List<ChatMessage> messages, CancellationToken cancellationToken);
}

public class OpenAiService : IOpenAiService, IDisposable
{
    private static readonly JsonSerializerOptions _JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    private readonly HttpClient _httpClient = new();
    
    public OpenAiService(string apiKey)
    {
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
    }
    
    public async Task<ChatMessage> GetCompletion(List<ChatMessage> messages, CancellationToken cancellationToken)
    {
        var openAiRequest = new ChatRequest()
        {
            Model = "gpt-3.5-turbo",
            Messages = messages
        };
        
        var jsonRequest = JsonSerializer.Serialize(openAiRequest, _JsonSerializerOptions);
        
        using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("chat/completions", content, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Error calling OpenAI API: {responseContent}");
            
            var result = JsonSerializer.Deserialize<ChatResponse>(responseContent, _JsonSerializerOptions) ?? throw new InvalidOperationException("Failed to deserialize OpenAI response");
            
            var firstChoice = result.Choices.FirstOrDefault();
            
            if (firstChoice?.Message is null)
                throw new InvalidOperationException("No message found in OpenAI response");

            return new ChatMessage()
            {
                Role = firstChoice.Message.Role,
                Content = firstChoice.Message.Content
            };
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Error calling OpenAI API: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}