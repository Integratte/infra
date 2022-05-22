using MediatR;

namespace Integratte.Infra.ModuloMediador.Comandos;
public abstract class ManipuladorDoComando<ComandoManipulado> : IManipuladorDoComando<ComandoManipulado>, INotificationHandler<ComandoManipulado> where ComandoManipulado : Comando
{
    public async Task Handle(ComandoManipulado evento, CancellationToken cancellationToken)
    {
        await Manipular(evento);

    }

    public abstract Task Manipular(ComandoManipulado evento);

}
