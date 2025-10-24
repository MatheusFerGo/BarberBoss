using BarberBoss.Domain;

namespace BarberBoss.Application.Interfaces;

public interface IExcelService
{
    byte[] GenerateBillingsExcel(IEnumerable<Billing> billings, decimal totalAmount);
}