using RagDemo.Entities;

namespace RagDemo.Services;

public class RetrievalService
{
    public List<DocumentChunk> GetRelevantChunks(
        List<DocumentChunk> chunks,
        string question)
    {
        var keywords =
            question
                .ToLower()
                .Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

        var scoredChunks =
            chunks
                .Select(chunk => new
                {
                    Chunk = chunk,

                    Score = keywords.Sum(keyword =>
                    {
                        if (chunk.ChunkText
                            .ToLower()
                            .Contains(keyword))
                        {
                            return 1;
                        }

                        return 0;
                    })
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.Chunk.ChunkIndex)
                .Take(3)
                .Select(x => x.Chunk)
                .ToList();

        return scoredChunks;
    }
}