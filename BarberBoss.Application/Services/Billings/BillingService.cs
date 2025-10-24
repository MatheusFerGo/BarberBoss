using BarberBoss.Application.Interfaces.Billings;
using BarberBoss.Domain;

namespace BarberBoss.Application.Services.Billings;

public class BillingService : IBillingService
{
    private readonly IBillingRepository _repository;

    public BillingService(IBillingRepository repository)
    {
        _repository = repository;
    }

    public async Task<BillingResponseDto> CreateAsync(CreateBillingDto dto)
    {
        var billing = new Billing(
            dto.Date,
            dto.BarberName,
            dto.ClientName,
            dto.ServiceName,
            dto.Amount,
            dto.PaymentMethod,
            BillingStatus.Pago,
            dto.Notes
            );

        await _repository.AddAsync(billing);

        return MapToResponseDto(billing);
    }

    public async Task<BillingResponseDto?> UpdateAsync(Guid id, UpdateBillingDto dto)
    {
        var billing = await _repository.GetByIdAsync(id);
        if (billing == null)
        {
            return null;
        }

        billing.Update(
            dto.Date,
            dto.BarberName,
            dto.ClientName,
            dto.ServiceName,
            dto.Amount,
            dto.PaymentMethod,
            dto.Status,
            dto.Notes
            );

        if (dto.Status == BillingStatus.Cancelado)
        {
            billing.Cancel();
        }

        await _repository.UpdateAsync(billing);

        return MapToResponseDto(billing);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var billing = await _repository.GetByIdAsync(id);
        if (billing == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<BillingResponseDto?> GetByIdAsync(Guid id)
    {
        var billing = await _repository.GetByIdAsync(id);
        return billing == null ? null : MapToResponseDto(billing);
    }

    public async Task<IEnumerable<BillingResponseDto>> GetAllAsync()
    {
        var billings = await _repository.GetAllAsync();
        return billings.Select(MapToResponseDto);
    }

    private BillingResponseDto MapToResponseDto(Billing billing)
    {
        return new BillingResponseDto
        {
            Id = billing.Id,
            Date = billing.Date,
            BarberName = billing.BarberName,
            ClientName = billing.ClientName,
            ServiceName = billing.ServiceName,
            Amount = billing.Amount,
            PaymentMethod = billing.PaymentMethod.ToString(),
            Status = billing.Status.ToString(),
            Notes = billing.Notes,
            CreatedAt = billing.CreatedAt,
            UpdatedAt = billing.UpdatedAt
        };
    }
}