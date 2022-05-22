using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Eventos;
using System.Linq;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.Fabricas;

internal class TesteDeEvento2
{
    private const string CONFIRMACAO_DE_PUBLICACAO = "Teste de Evento Publicado";

    public static async Task<bool> Testar(Mediador mediador)
    {
        await mediador.PublicarEvento(CriarEvento());
        return EventoFoiPublicado(mediador);

    }

    private static Evento CriarEvento() { return new EventoTeste(); }
    private static bool EventoFoiPublicado(Mediador mediador)
    {
        var notificacoes = mediador.Notificacoes;
        return notificacoes.Any(x => x.Mensagem == CONFIRMACAO_DE_PUBLICACAO);

    }

    private class EventoTeste : Evento { }
    private class ManipuladorDeEventoTeste : ManipuladorDoEvento<EventoTeste>
    {
        private readonly Mediador _mediador;

        public ManipuladorDeEventoTeste(Mediador mediador)
        {
            _mediador = mediador;

        }

        public override Task Manipular(EventoTeste evento)
        {
            _mediador.AdicionarNotificacao(CONFIRMACAO_DE_PUBLICACAO);
            return Task.CompletedTask;

        }

    }

}
