namespace RagDemo.Services;

public class ChunkService
{
    public List<string> SplitIntoChunks(
        string text,
        int chunkSize = 500)
    {
        var chunks = new List<string>();

        for (int i = 0; i < text.Length; i += chunkSize)
        {
            chunks.Add(
                text.Substring(
                    i,
                    Math.Min(
                        chunkSize,
                        text.Length - i)));
        }

        return chunks;
    }
}