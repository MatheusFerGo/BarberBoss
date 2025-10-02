namespace BarberBoss.Domain.Events
{
    public interface IDomainEvent
    {
        public DateTime OcurredOn { get; }
    }
}
