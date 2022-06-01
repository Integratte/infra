namespace Integratte.Infra.ModuloMediador.Notificacoes;

public interface INotificacao
{
    string Mensagem { get; }
    bool PodeExibirParaUsuario { get; }
    TipoDeNotificacaoEnum TipoDeNotificacaoEnum { get; }
    string DescricaoDoTipoDeNotificacao { get; }

}
