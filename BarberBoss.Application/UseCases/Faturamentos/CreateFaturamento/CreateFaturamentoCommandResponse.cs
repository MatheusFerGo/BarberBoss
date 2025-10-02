namespace BarberBoss.Application.UseCases.Faturamentos.CreateFaturamento
{
    public class CreateFaturamentoCommandResponse
    {
        public int FaturamentoId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
    }
}