using Microsoft.EntityFrameworkCore;
using RagDemo.Data;
using RagDemo.Middleware;
using RagDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<EmbeddingService>();

builder.Services.AddScoped<OpenAiService>();
builder.Services.AddScoped<RetrievalService>();
builder.Services.AddScoped<ChunkService>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
            .GetConnectionString(
                "DefaultConnection")));

var app = builder.Build();

// Configure HTTP pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();