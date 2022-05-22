using MediatR;

namespace Integratte.Infra.ModuloMediador.Comandos;
public abstract class ManipuladorDoComando<ComandoManipulado> : INotificationHandler<ComandoManipulado> where ComandoManipulado : Comando
{
    public async Task Handle(ComandoManipulado evento, CancellationToken cancellationToken)
    {
        await Manipular(evento);

    }

    public abstract Task Manipular(ComandoManipulado evento);

}


public abstract class ManipuladorDoComando<ComandoManipulado, RetornoDoComando> : IRequestHandler<ComandoManipulado, RetornoDoComando> where RetornoDoComando : IRetornoDoComando where ComandoManipulado : Comando<RetornoDoComando>
{
    public async Task<RetornoDoComando> Handle(ComandoManipulado comando, CancellationToken cancellationToken)
    {
        return await Manipular(comando);

    }

    public abstract Task<RetornoDoComando> Manipular(ComandoManipulado comando);

}
