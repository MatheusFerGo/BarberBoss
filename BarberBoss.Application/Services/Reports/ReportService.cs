using BarberBoss.Application.Interfaces.Reports;
using BarberBoss.Domain;

namespace BarberBoss.Application.Services.Reports;

public class ReportService : IReportService
{
    private readonly IBillingRepository _billingRepository;
    private readonly IExcelService _excelService;
    private readonly IPdfService _pdfService;

    public ReportService(IBillingRepository billingRepository, IExcelService serviceRepository, IPdfService pdfService)
    {
        _billingRepository = billingRepository;
        _excelService = serviceRepository;
        _pdfService = pdfService;
    }

    public async Task<byte[]> GenerateExcelReportAsync(DateOnly startDate, DateOnly endDate)
    {
        var paidBillings = await _billingRepository.GetPaidBillingsInPeriodAsync(startDate, endDate);

        var totalAmount = paidBillings.Sum(b => b.Amount);

        var fileBytes = _excelService.GenerateBillingsExcel(paidBillings, totalAmount);

        return fileBytes;
    }

    public async Task<byte[]> GeneratePdfReportAsync(DateOnly startDate, DateOnly endDate)
    {
        var paidBillings = await _billingRepository.GetPaidBillingsInPeriodAsync(startDate, endDate);

        var totalAmount = paidBillings.Sum(b => b.Amount);

        var fileBytes = _pdfService.GenerateBillingsPdf(paidBillings, totalAmount, startDate, endDate);

        return fileBytes;
    }
}
