using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RagDemo.Data;
using RagDemo.Models;
using RagDemo.Services;

namespace RagDemo.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly OpenAiService _openAiService;

    private readonly AppDbContext _context;
    private readonly RetrievalService _retrievalService;

    public ChatController(
        OpenAiService openAiService, RetrievalService retrievalService,
        AppDbContext context)
    {
        _openAiService = openAiService;

        _context = context;
        _retrievalService = retrievalService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskQuestion(
     [FromBody] QuestionRequest request)
    {
        var document =
            await _context.Documents
                .Include(x => x.Chunks)
                .FirstOrDefaultAsync(
                    x => x.Id == request.DocumentId);

        if (document == null)
        {
            return NotFound("Document not found.");
        }

        // Retrieve relevant chunks
        var relevantChunks =
            _retrievalService.GetRelevantChunks(
                document.Chunks.ToList(),
                request.Question);

        // Build context
        var context =
            string.Join(
                "\n\n",
                relevantChunks.Select(
                    x => x.ChunkText));

        // Ask AI
        var answer =
            await _openAiService.AskQuestionAsync(
                context,
                request.Question);

        // Debug info
        var retrievedChunks =
            relevantChunks.Select(x => new
            {
                x.ChunkIndex,

                x.ChunkText
            });

        return Ok(new
        {
            retrievedChunks,

            answer
        });
    }
}
