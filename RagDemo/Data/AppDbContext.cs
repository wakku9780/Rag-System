using Microsoft.EntityFrameworkCore;
using RagDemo.Entities;

namespace RagDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; }

    public DbSet<DocumentChunk> DocumentChunks
    {
        get;
        set;
    }
}