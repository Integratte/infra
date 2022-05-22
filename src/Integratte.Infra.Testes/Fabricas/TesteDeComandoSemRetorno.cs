using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Comandos;
using System.Linq;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.Fabricas;

internal class TesteDeComandoSemRetorno
{
    private const string CONFIRMACAO_DE_EXECUCAO = "Teste de Comando Executado";

    public static async Task<bool> Testar(Mediador mediador)
    {
        await mediador.ExecutarComando(CriarComando());
        return ComandoFoiExecutado(mediador);

    }

    private static Comando CriarComando() { return new TesteDeComando(); }
    private static bool ComandoFoiExecutado(Mediador mediador)
    {
        var notificacoes = mediador.Notificacoes;
        return notificacoes.Any(x => x.Mensagem == CONFIRMACAO_DE_EXECUCAO);

    }

    private class TesteDeComando : Comando { }
    private class ManipuladorDoTesteDeComando : ManipuladorDoComando<TesteDeComando>
    {
        private readonly Mediador _mediador;

        public ManipuladorDoTesteDeComando(Mediador mediador)
        {
            _mediador = mediador;

        }

        public override Task Manipular(TesteDeComando comando)
        {
            _mediador.AdicionarNotificacao(CONFIRMACAO_DE_EXECUCAO);
            return Task.CompletedTask;

        }

    }

}
