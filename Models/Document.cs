namespace TablePrinterXTech.Models;
public class Document
{
    public string FileName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public IEnumerable<DocumentDataRow> Data { get; set; } = Enumerable.Empty<DocumentDataRow>();
}

public class DocumentDataRow()
{
    public int Number { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}