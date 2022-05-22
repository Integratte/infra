using Integratte.Infra.ModuloMediador.Consultas;
using MediatR;

namespace Integratte.Infra.MediatR.Consultas;

internal class ConsultasDoMediador : IConsultasDoMediador
{
    private readonly IMediator _mediator;

    public ConsultasDoMediador(IMediator mediator)
    {
        _mediator = mediator;

    }

    public async Task<RetornoT?> Consultar<RetornoT>(IConsulta<RetornoT> consulta) where RetornoT : IRetornoDaConsulta
    {
        return await ExecutarConsulta(consulta as Consulta<RetornoT>);

    }
    public async Task<RetornoT?> ExecutarConsulta<RetornoT>(Consulta<RetornoT>? consulta) where RetornoT : IRetornoDaConsulta
    {
        if (consulta == null) return default;
        return await _mediator.Send(consulta);

    }

}