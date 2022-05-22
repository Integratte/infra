using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Consultas;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.Fabricas;

internal class TesteDeConsulta
{
    public static async Task<bool> Testar(Mediador mediador)
    {
        var retorno = await mediador.Consultar(CriarConsulta());
        return retorno != null && mediador.SemImpedimentos;

    }

    private static Consulta<RetornoDeConsultaTeste> CriarConsulta() { return new ConsultaTeste(); }

    private class ConsultaTeste : Consulta<RetornoDeConsultaTeste> { }
    private class RetornoDeConsultaTeste : IRetornoDaConsulta { }
    private class ManipuladorDaConsultaTeste : ManipuladorDaConsulta<ConsultaTeste, RetornoDeConsultaTeste>
    {
        public override Task<RetornoDeConsultaTeste> Manipular(ConsultaTeste comando)
        {
            return Task.FromResult(new RetornoDeConsultaTeste());

        }

    }

}