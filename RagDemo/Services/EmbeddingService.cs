using Newtonsoft.Json;
using System.Text;

namespace RagDemo.Services;

public class EmbeddingService
{
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public EmbeddingService(
        IConfiguration configuration,
        HttpClient httpClient)
    {
        _configuration = configuration;

        _httpClient = httpClient;
    }

    public async Task<string> GenerateEmbedding(
        string text)
    {
        var apiKey =
            _configuration["Groq:ApiKey"];

        var body = new
        {
            model = "nomic-embed-text-v1.5",

            input = text
        };

        var json =
            JsonConvert.SerializeObject(body);

        var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.groq.com/openai/v1/embeddings");

        request.Headers.Add(
            "Authorization",
            $"Bearer {apiKey}");

        request.Content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        var response =
            await _httpClient.SendAsync(request);

        var responseJson =
            await response.Content.ReadAsStringAsync();

        return responseJson;
    }
}