namespace Integratte.Infra.ModuloMediador.Notificacoes;

public interface INotificacao
{
    string Mensagem { get; }
    bool PodeExibirMensagemParaUsuario { get; }
    bool RequisicaoInvalida { get; }
    bool ProvocaInterrupcaoDoSistema { get; }

}
