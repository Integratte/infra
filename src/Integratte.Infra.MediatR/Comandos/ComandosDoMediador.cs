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

    private async Task ExecutarComando(Comando? comando)
    {
        if (comando == null) return;

        await _mediator.Publish(comando);

    }

    public async Task Executar(IComando comando)
    {
        await ExecutarComando(comando as Comando);

    }

}
