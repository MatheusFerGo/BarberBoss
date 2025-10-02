namespace BarberBoss.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
