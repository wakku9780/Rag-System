using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RagDemo.Data;
using RagDemo.Entities;
using RagDemo.Services;

namespace RagDemo.Controllers;

[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    private readonly PdfService _pdfService;
    private readonly ChunkService _chunkService;
    //private readonly EmbeddingService _embeddingService;

    private readonly AppDbContext _context;

    public PdfController(
        PdfService pdfService,
        AppDbContext context, ChunkService chunkService) //EmbeddingService embeddingService)
    {
        _pdfService = pdfService;

        _context = context;
        _chunkService = chunkService;
       // _embeddingService = embeddingService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid PDF file.");
        }

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Uploads");

        Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(
            uploadsFolder,
            file.FileName);

        // Save file
        using (var stream = new FileStream(
            filePath,
            FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Extract text
        var extractedText =
            _pdfService.ExtractText(filePath);

        // Create document entity
        var document = new Document
        {
            Id = Guid.NewGuid(),

            FileName = file.FileName,

            ExtractedText = extractedText,

            UploadedAt = DateTime.UtcNow
        };

        // Save to database
        _context.Documents.Add(document);

        await _context.SaveChangesAsync();

        var chunks =
    _chunkService.SplitIntoChunks(
        extractedText);

        int index = 0;

        foreach (var chunk in chunks)
        {
            //var embedding =
            //    await _embeddingService
            //        .GenerateEmbedding(chunk);

            var documentChunk =
                new DocumentChunk
                {
                    Id = Guid.NewGuid(),

                    DocumentId = document.Id,

                    ChunkText = chunk,

                    ChunkIndex = index++

                    //Embedding =
                    //    JsonConvert.SerializeObject(
                    //        embedding)
                };

            _context.DocumentChunks.Add(
                documentChunk);
        }

        await _context.SaveChangesAsync();

        // Return documentId
        return Ok(new
        {
            message = "PDF uploaded successfully.",

            documentId = document.Id
        });
    }
}