namespace Integratte.Infra.ModuloMediador.Notificacoes;

public interface INotificacao
{
    string Mensagem { get; }
    bool PodeExibirMensagemParaUsuario { get; }
    TipoDeNotificacaoEnum TipoDeNotificacaoEnum { get; }
    string DescricaoDoTipoDeNotificacao { get; }

}
