1. Project Title
# AI PDF Question Answering System

2. Short Description
An AI-powered document question-answering system built using ASP.NET Core Web API, SQL Server, EF Core, and LLM integration.

The application allows users to upload PDF documents, extract text, perform chunk-based retrieval, and ask contextual questions from uploaded documents.

3. Features
## Features

- PDF Upload API
- PDF Text Extraction
- SQL Server Storage
- Chunk-based Retrieval
- AI-powered Question Answering
- Retrieval-Augmented Flow
- Swagger API Testing
- Global Exception Handling
- EF Core Integration

4. Tech Stack
## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- Groq API / LLM Integration
- PdfPig


5. Architecture Flow
## Architecture Flow

PDF Upload
→ Text Extraction
→ Chunking
→ SQL Storage
→ Retrieval
→ AI Context Building
→ LLM Response

6. API Endpoints
## API Endpoints

### Upload PDF
POST /api/pdf/upload

### Ask Question
POST /api/chat/ask

7. Future Improvements
## Future Improvements

- Vector Database Integration
- Embeddings-based Retrieval
- React Frontend
- User Authentication
- Multi-document Chat
- Semantic Search
