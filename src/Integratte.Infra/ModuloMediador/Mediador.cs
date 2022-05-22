using Integratte.Infra.ModuloMediador.Eventos;
using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.ModuloMediador;

public abstract class Mediador
{
    protected readonly NotificacoesDoMediador _notificacoes;
    protected readonly IEventosDoMediador _eventos;

    protected Mediador(NotificacoesDoMediador notificacoes, IEventosDoMediador eventos)
    {
        _notificacoes = notificacoes;
        _eventos = eventos;

    }

    public INotificacao[] Notificacoes => _notificacoes.Listar;
    public void AdicionarNotificacao(string mensagem, bool exibirParaUsuario = true, bool requisicaoInvalida = true, bool provocaInterrupcaoDoSistema = false)
    {
        _notificacoes.Adicionar(mensagem, exibirParaUsuario, requisicaoInvalida, provocaInterrupcaoDoSistema);

    }

    public async Task PublicarEvento(IEvento evento)
    {
        if (_notificacoes.FoiInterrompido)
            return;

        await _eventos.Publicar(evento);

    }

}
