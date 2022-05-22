using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Eventos;
using System.Linq;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.Fabricas;

internal class FabricaDeTesteEvento
{
    private const string NotificacaoDeConfirmacaoDePublicacao = "Teste de Evento Publicado";

    public static async Task<bool> Testar(Mediador mediador)
    {
        await mediador.PublicarEvento(CriarEvento());
        return EventoFoiPublicado(mediador);

    }

    private static Evento CriarEvento() { return new TesteDeEvento(); }
    private static bool EventoFoiPublicado(Mediador mediador)
    {
        var notificacoes = mediador.Notificacoes;
        return notificacoes.Any(x => x.Mensagem == NotificacaoDeConfirmacaoDePublicacao);

    }

    private class TesteDeEvento : Evento { }
    private class ManipuladorDoTesteDeEvento : ManipuladorDoEvento<TesteDeEvento>
    {
        private readonly Mediador _mediador;

        public ManipuladorDoTesteDeEvento(Mediador mediador)
        {
            _mediador = mediador;

        }

        public override Task Manipular(TesteDeEvento evento)
        {
            _mediador.AdicionarNotificacao(NotificacaoDeConfirmacaoDePublicacao);
            return Task.CompletedTask;

        }
    }

}
