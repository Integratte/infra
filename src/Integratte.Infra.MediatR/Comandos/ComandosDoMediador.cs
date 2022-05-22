using Integratte.Infra.ModuloMediador.Comandos;
using MediatR;

namespace Integratte.Infra.MediatR.Comandos;

internal class ComandosDoMediador : IComandosDoMediador
{
    private readonly IMediator _mediator;

    public ComandosDoMediador(IMediator mediator)
    {
        _mediator = mediator;

    }


    public async Task Executar(IComando comando)
    {
        await ExecutarComando(comando as Comando);

    }
    private async Task ExecutarComando(Comando? comando)
    {
        if (comando == null) return;
        await _mediator.Publish(comando);

    }


    public async Task<RetornoT?> Executar<RetornoT>(IComando<RetornoT> comando) where RetornoT : IRetornoDoComando
    {
        return await ExecutarComando(comando as Comando<RetornoT>);

    }
    public async Task<RetornoT?> ExecutarComando<RetornoT>(Comando<RetornoT>? comando) where RetornoT : IRetornoDoComando
    {
        if (comando == null) return default;
        return await _mediator.Send(comando);

    }

}