namespace BarberBoss.Application;

public interface IReportService
{
    Task<byte[]> GenerateExcelReportAsync(DateOnly startDate, DateOnly endDate);
}