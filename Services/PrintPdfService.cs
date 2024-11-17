using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace TablePrinterXTech.Services;
public class PrintPdfService : IPrintService
{
    public void Print(Models.Document document)
    {
        Document.Create(container =>
        {
            Compose(container, document);
        }).GeneratePdf(document.FileName);
    }

    private IDocumentContainer Compose(IDocumentContainer container, Models.Document document)
    {
        container.Page(page =>
        {
            page.MarginHorizontal(2, Unit.Centimetre);
            page.MarginVertical(1, Unit.Centimetre);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(12));


            page.Content().Element(_cont =>
            {
                _cont.Column(column =>
                {
                    column.Item().Text(document.Title).Bold().AlignCenter();
                    column.Spacing(10);
                    column.Item().Element(x => ComposeTable(x, document.Data));
                });
            });
        });
        return container;
    }

    private void ComposeTable(IContainer container, IEnumerable<Models.DocumentDataRow> data)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(80);
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Номер");
                header.Cell().Element(CellStyle).Text("Фамилия");
                header.Cell().Element(CellStyle).Text("Имя");

                static IContainer CellStyle(IContainer container)
                {
                    return container.Background(Colors.Grey.Lighten2).DefaultTextStyle(x => x.Bold()).Border(1).BorderColor(Colors.Grey.Lighten1).PaddingHorizontal(5).PaddingVertical(2);
                }
            });

            foreach (var row in data)
            {
                table.Cell().Element(CellStyle).Text(row.Number.ToString());
                table.Cell().Element(CellStyle).Text(row.LastName);
                table.Cell().Element(CellStyle).Text(row.FirstName);

                static IContainer CellStyle(IContainer container)
                {
                    return container.Border(1).BorderColor(Colors.Grey.Lighten1).PaddingHorizontal(5).PaddingVertical(2);
                }
            }
        });
    }
}
