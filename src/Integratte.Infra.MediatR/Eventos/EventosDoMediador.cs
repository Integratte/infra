using Integratte.Infra.ModuloMediador.Eventos;
using MediatR;

namespace Integratte.Infra.MediatR.Eventos;

internal class EventosDoMediador : IEventosDoMediador
{
    private readonly IMediator _mediator;

    public EventosDoMediador(IMediator mediator)
    {
        _mediator = mediator;

    }

    private async Task PublicarEvento(Evento? evento)
    {
        if (evento == null) return;

        await _mediator.Publish(evento);

    }

    public async Task Publicar(IEvento evento)
    {
        await PublicarEvento(evento as Evento);

    }

}
