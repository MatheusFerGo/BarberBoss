using BarberBoss.Domain;

namespace BarberBoss.Application.Interfaces.Reports;

public interface IExcelService
{
    byte[] GenerateBillingsExcel(IEnumerable<Billing> billings, decimal totalAmount);
}