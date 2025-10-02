using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Interfaces;
using MediatR;

namespace BarberBoss.Application.UseCases.Faturamentos.CreateFaturamento
{
    public class CreateFaturamentoCommandHandler : IRequestHandler<CreateFaturamentoCommand,CreateFaturamentoCommandResponse>
    {
        private readonly IFaturamentoRepository _faturamentoRepository;
        private readonly IUnityOfWork _unitOfWork;

        public CreateFaturamentoCommandHandler(IFaturamentoRepository faturamentoRepository, IUnityOfWork unityOfWork)
        {
            _faturamentoRepository = faturamentoRepository;
            _unitOfWork = unityOfWork;
        }

        public async Task<CreateFaturamentoCommandResponse> Handle(CreateFaturamentoCommand request, CancellationToken cancellationToken)
        {
            var faturamento = Faturamento.Create(
                request.NomeCliente,
                request.Data,
                request.ValorTotal,
                request.ServicosPrestados
                );

            _faturamentoRepository.Add(faturamento);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CreateFaturamentoCommandResponse
            {
                FaturamentoId = faturamento.FaturamentoId,
                NomeCliente = faturamento.NomeCliente
            };
        }
    }
}
