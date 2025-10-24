namespace BarberBoss.Application.Interfaces.Reports;

public interface IReportService
{
    Task<byte[]> GenerateExcelReportAsync(DateOnly startDate, DateOnly endDate);

    Task<byte[]> GeneratePdfReportAsync(DateOnly startDate, DateOnly endDate);
}