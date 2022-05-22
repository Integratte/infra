using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Comandos;
using System.Linq;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.Fabricas;

internal class TesteDeComandoComRetorno
{
    public static async Task<bool> Testar(Mediador mediador)
    {
        var retorno = await mediador.ExecutarComando(CriarComando());
        return retorno != null && mediador.SemImpedimentos;

    }

    private static Comando<RetornoDeTesteDeComando> CriarComando() { return new TesteDeComando(); }

    private class TesteDeComando : Comando<RetornoDeTesteDeComando> { }
    private class RetornoDeTesteDeComando : IRetornoDoComando { }
    private class ManipuladorDoTesteDeComando : ManipuladorDoComando<TesteDeComando, RetornoDeTesteDeComando>
    {
        public override Task<RetornoDeTesteDeComando> Manipular(TesteDeComando comando)
        {
            return Task.FromResult(new RetornoDeTesteDeComando());

        }

    }

}