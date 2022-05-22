using MediatR;

namespace Integratte.Infra.ModuloMediador.Consultas;

public abstract class ManipuladorDaConsulta<ConsultaManipulada, RetornoDaConsulta> : IRequestHandler<ConsultaManipulada, RetornoDaConsulta> where RetornoDaConsulta : IRetornoDaConsulta where ConsultaManipulada : Consulta<RetornoDaConsulta>
{
    public async Task<RetornoDaConsulta> Handle(ConsultaManipulada comando, CancellationToken cancellationToken)
    {
        return await Manipular(comando);

    }

    public abstract Task<RetornoDaConsulta> Manipular(ConsultaManipulada comando);

}
