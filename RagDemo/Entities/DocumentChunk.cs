namespace RagDemo.Entities;

public class DocumentChunk
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string ChunkText { get; set; }

    public int ChunkIndex { get; set; }

    public string? Embedding { get; set; }

    // Navigation property
    public Document Document { get; set; }
}