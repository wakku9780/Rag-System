using System.Text;
using Newtonsoft.Json;
using RagDemo.Models;

namespace RagDemo.Services;

public class OpenAiService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public OpenAiService(
        IConfiguration configuration,
        HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<string> AskQuestionAsync(
        string pdfText,
        string question)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];

        var prompt = $@"
Answer only from the provided PDF content.

PDF Content:
{pdfText}

Question:
{question}
";

        var requestBody = new ChatRequest
        {
            model = "llama-3.3-70b-versatile",
            messages = new List<Message>
            {
                new Message
                {
                    role = "user",
                    content = prompt
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestBody);

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.groq.com/openai/v1/chat/completions");

        request.Headers.Add(
            "Authorization",
            $"Bearer {apiKey}");

        request.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"OpenAI API Error: {error}");
        }

        var responseJson =
            await response.Content.ReadAsStringAsync();

        var result =
            JsonConvert.DeserializeObject<OpenAiResponse>(
                responseJson);

        return result?.choices?[0]?.message?.content
               ?? "No response generated.";
    }
}