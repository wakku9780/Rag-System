namespace RagDemo.Models
{
    public class OpenAiResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }
}
