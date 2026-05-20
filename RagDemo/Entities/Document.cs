namespace RagDemo.Entities;

public class Document
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string ExtractedText { get; set; }

    public DateTime UploadedAt { get; set; }

    public ICollection<DocumentChunk> Chunks
    {
        get;
        set;
    }
}