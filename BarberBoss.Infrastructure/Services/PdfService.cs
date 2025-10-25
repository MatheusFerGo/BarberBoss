using BarberBoss.Application.Interfaces.Reports;
using BarberBoss.Domain;
using DocumentFormat.OpenXml.Spreadsheet;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BarberBoss.Infrastructure;

public class PdfService : IPdfService
{
    public byte[] GenerateBillingsPdf(IEnumerable<Billing> billings, decimal totalAmount, DateOnly startDate, DateOnly endDate)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = new BillingReportDocument(billings, totalAmount, startDate, endDate);

        return document.GeneratePdf();
    }
}

internal class BillingReportDocument : IDocument
{
    private readonly IEnumerable<Billing> _billings;
    private readonly decimal _totalAmount;
    private readonly DateOnly _startDate;
    private readonly DateOnly _endDate;

    public BillingReportDocument(IEnumerable<Billing> billings, decimal totalAmount, DateOnly startDate, DateOnly endDate)
    {
        _billings = billings;
        _totalAmount = totalAmount;
        _startDate = startDate;
        _endDate = endDate;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().Text("BarberBiss - Relatório de Faturamento")
                   .Bold().FontSize(20);

                col.Item().Text($"Período: {_startDate: dd/MM/yyyy} a {_endDate: dd/MM/yyyy}");
            });

            row.ConstantItem(100).Height(50);
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(col =>
        {
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);      // Data
                    columns.RelativeColumn(2.5f);   // Cliente
                    columns.RelativeColumn(2);      // Barbeiro
                    columns.RelativeColumn(2);      // Serviço
                    columns.ConstantColumn(80);     //Valor
                });

                table.Header(header =>
                {
                    header.Cell().Background("#EEEEEE").Padding(5).Text("Data");
                    header.Cell().Background("#EEEEEE").Padding(5).Text("Cliente");
                    header.Cell().Background("#EEEEEE").Padding(5).Text("Barbeiro");
                    header.Cell().Background("#EEEEEE").Padding(5).Text("Serviço");
                    header.Cell().Background("#EEEEEE").Padding(5).AlignRight().Text("Valor");
                });

                foreach (var billing in _billings)
                {
                    table.Cell().BorderBottom(1).BorderColor("#F5F5F5").Padding(5).Text(billing.Date.ToString("dd/MM/yy"));
                    table.Cell().BorderBottom(1).BorderColor("#F5F5F5").Padding(5).Text(billing.ClientName);
                    table.Cell().BorderBottom(1).BorderColor("#F5F5F5").Padding(5).Text(billing.BarberName);
                    table.Cell().BorderBottom(1).BorderColor("#F5F5F5").Padding(5).Text(billing.ServiceName);
                    table.Cell().BorderBottom(1).BorderColor("#F5F5F5").Padding(5).AlignRight().Text(billing.Amount.ToString("C", new System.Globalization.CultureInfo("pt-BR")));
                }
            });

            col.Item().AlignRight().PaddingTop(10).Row(row =>
            {
                row.RelativeItem().AlignRight().Text("Total (Pagos):").Bold().FontSize(14);
                row.ConstantItem(80).AlignRight().Text(_totalAmount.ToString("C", new System.Globalization.CultureInfo("pt-BR"))).Bold().FontSize(14);
            });
        });
    }

    void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("Página ");
            text.CurrentPageNumber();
        });
    }
}
