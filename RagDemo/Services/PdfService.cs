using UglyToad.PdfPig;

namespace RagDemo.Services;

public class PdfService
{
    public string ExtractText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("PDF file not found.");
        }

        using var document = PdfDocument.Open(filePath);

        string text = "";

        foreach (var page in document.GetPages())
        {
            text += page.Text;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new Exception("No text found in PDF.");
        }

        return text;
    }
}