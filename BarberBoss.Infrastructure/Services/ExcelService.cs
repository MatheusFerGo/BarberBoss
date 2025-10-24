using BarberBoss.Application.Interfaces;
using BarberBoss.Domain;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace BarberBoss.Infrastructure;

public class ExcelService : IExcelService
{
    public byte[] GenerateBillingsExcel (IEnumerable<Billing> billings, decimal totalAmount)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Faturamentos");

            worksheet.Cell(1, 1).Value = "Data";
            worksheet.Cell(1, 2).Value = "Cliente";
            worksheet.Cell(1, 3).Value = "Barbeiro";
            worksheet.Cell(1, 4).Value = "Serviço";
            worksheet.Cell(1, 5).Value = "Forma de pagamento";
            worksheet.Cell(1, 6).Value = "Valor";

            var headerRange = worksheet.Range("A1:F1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // --- Dados (Linhas) ---
            int currentRow = 2;
            foreach (var billing in billings)
            {
                worksheet.Cell(currentRow, 1).Value = billing.Date.ToString("dd/MM/yyyy");
                worksheet.Cell(currentRow, 2).Value = billing.ClientName;
                worksheet.Cell(currentRow, 3).Value = billing.BarberName;
                worksheet.Cell(currentRow, 4).Value = billing.ServiceName;
                worksheet.Cell(currentRow, 5).Value = billing.PaymentMethod.ToString();
                worksheet.Cell(currentRow, 6).Value = billing.Amount;
                currentRow++;
            }

            // --- Total ---
            worksheet.Cell(currentRow, 1).Value = "Total:";
            worksheet.Cell(currentRow, 6).Value = totalAmount;
            worksheet.Range(currentRow, 5, currentRow, 6).Style.Font.Bold = true;
            worksheet.Range(currentRow, 5, currentRow, 6).Style.Fill.BackgroundColor = XLColor.LightYellow;

            worksheet.Column(6).Style.NumberFormat.Format = "R$ #,##0.00";
            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}