using BarberBoss.Application.Interfaces.Reports;
using BarberBoss.Application.Services.Reports;
using BarberBoss.Domain;
using BarberBoss.Domain.Enums;
using Moq;

namespace BarberBoss.Application.Tests.Services;

public class ReportServiceTests
{
    private readonly Mock<IBillingRepository> _mockBillingRepo;
    private readonly Mock<IExcelService> _mockExcelService;
    private readonly Mock<IPdfService> _mockPdfService;
    private readonly ReportService _reportService;

    public ReportServiceTests()
    {
        _mockBillingRepo = new Mock<IBillingRepository>();
        _mockExcelService = new Mock<IExcelService>();
        _mockPdfService = new Mock<IPdfService>();

        _reportService = new ReportService(
            _mockBillingRepo.Object,
            _mockExcelService.Object,
            _mockPdfService.Object);
    }

    [Fact]
    public async Task Should_SumTotalAmountCorrectly_When_GeneratingReport()
    {
        var startDate = new DateOnly(2025, 10, 1);
        var endDate = new DateOnly(2025, 10, 31);

        var fakeBillings = new List<Billing>
        {
            TestBillingFactory(100.0m),
            TestBillingFactory(50.50m),
            TestBillingFactory(25.0m)
        };

        _mockBillingRepo
            .Setup(repo => repo.GetPaidBillingsInPeriodAsync(startDate, endDate))
            .ReturnsAsync(fakeBillings);

        _mockExcelService
            .Setup(excel => excel.GenerateBillingsExcel(
                It.IsAny<IEnumerable<Billing>>(),
                It.IsAny<decimal>()))
            .Returns(new byte[0]); 

        await _reportService.GenerateExcelReportAsync(startDate, endDate);

        _mockExcelService.Verify(excel =>
            excel.GenerateBillingsExcel(
                It.IsAny<IEnumerable<Billing>>(), 
                175.50m),
            Times.Once); 
    }

    private Billing TestBillingFactory(decimal amount)
    {
        return new Billing(
            new DateOnly(2025, 10, 25), "Barbeiro", "Cliente", "Serviço",
            amount, PaymentMethod.Pix, BillingStatus.Pago, null);
    }

}