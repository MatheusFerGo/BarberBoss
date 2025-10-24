namespace BarberBoss.Application;

public interface IReportService
{
    Task<byte[]> GenerateWeeklyExcelReportAsync(DateOnly startDate, DateOnly endDate);
}