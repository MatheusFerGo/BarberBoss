using MediatR;

namespace BarberBoss.Application.UseCases.Faturamentos.CreateFaturamento
{
    public class CreateFaturamentoCommand : IRequest<CreateFaturamentoCommandResponse>
    {
        public string NomeCliente { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string ServicosPrestados { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
