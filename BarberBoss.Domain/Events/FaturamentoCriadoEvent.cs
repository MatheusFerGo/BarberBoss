namespace BarberBoss.Domain.Events
{
    public class FaturamentoCriadoEvent : IDomainEvent
    {
        public FaturamentoCriadoEvent(int faturamentoId, string nomeCliente, decimal valorTotal)
        {
            FaturamentoId = faturamentoId;
            NomeCliente = nomeCliente;
            ValorTotal = valorTotal;
            OcurredOn = DateTime.UtcNow;
        }

        public int FaturamentoId { get; }
        public string NomeCliente { get; }
        public decimal ValorTotal { get; }
        public DateTime OcurredOn { get; }
    }
}
