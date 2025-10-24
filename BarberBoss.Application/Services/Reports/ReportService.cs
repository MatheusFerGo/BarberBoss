using BarberBoss.Application.Interfaces;
using BarberBoss.Domain;

namespace BarberBoss.Application.Services.Reports;

public class ReportService : IReportService
{
    private readonly IBillingRepository _billingRepository;
    private readonly IExcelService _excelService;

    public ReportService(IBillingRepository billingRepository, IExcelService serviceRepository)
    {
        _billingRepository = billingRepository;
        _excelService = serviceRepository;
    }

    public async Task<byte[]> GenerateWeeklyExcelReportAsync(DateOnly startDate, DateOnly endDate)
    {
        var paidBillings = await _billingRepository.GetPaidBillingsInPeriodAsync(startDate, endDate);

        var totalAmount = paidBillings.Sum(b => b.Amount);

        var fileBytes = _excelService.GenerateBillingsExcel(paidBillings, totalAmount);

        return fileBytes;
    }
}
