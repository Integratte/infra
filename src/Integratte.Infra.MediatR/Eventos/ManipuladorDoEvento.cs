using MediatR;

namespace Integratte.Infra.ModuloMediador.Eventos;
public abstract class ManipuladorDoEvento<EventoManipulado> : IManipuladorDoEvento<EventoManipulado>, INotificationHandler<EventoManipulado> where EventoManipulado : Evento
{
    public async Task Handle(EventoManipulado evento, CancellationToken cancellationToken)
    {
        await Manipular(evento);

    }

    public abstract Task Manipular(EventoManipulado evento);

}
