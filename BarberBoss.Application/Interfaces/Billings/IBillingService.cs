namespace BarberBoss.Application.Interfaces.Billings;

public interface IBillingService
{
    Task<BillingResponseDto> CreateAsync(CreateBillingDto dto);
    Task<BillingResponseDto> UpdateAsync(Guid id, UpdateBillingDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<BillingResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<BillingResponseDto>> GetAllAsync();
}
