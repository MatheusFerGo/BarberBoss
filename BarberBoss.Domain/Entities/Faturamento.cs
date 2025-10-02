using BarberBoss.Domain.Events;

namespace BarberBoss.Domain.Entities
{
    public sealed class Faturamento : Entity
    {
        private Faturamento() { }

        public int FaturamentoId { get; private set; }
        public string NomeCliente { get; private set; } = string.Empty;
        public DateTime Data { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string ServicosPrestados { get; private set; } = string.Empty;


        public static Faturamento Create(string nomeCliente, DateTime data, decimal valorTotal, string servicosPrestados)
        {
            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new ArgumentException("Nome do cliente é obrigatório");
            if (valorTotal <= 0)
                throw new ArgumentException("Valor total deve ser maior que zero.");

            var faturamento = new Faturamento
            {
                NomeCliente = nomeCliente,
                Data = data,
                ValorTotal = valorTotal,
                ServicosPrestados = servicosPrestados
            };

            faturamento.AddDomainEvent(new FaturamentoCriadoEvent(faturamento.FaturamentoId, faturamento.NomeCliente, faturamento.ValorTotal));

            return faturamento;
        }
    }
}
