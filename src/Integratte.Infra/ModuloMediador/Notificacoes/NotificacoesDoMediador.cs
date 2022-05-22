namespace Integratte.Infra.ModuloMediador.Notificacoes;

public abstract class NotificacoesDoMediador
{
    protected List<INotificacao> Notificacoes { get; set; } = new();

    public INotificacao[] Listar => Notificacoes.ToArray();
    public bool ContemNotificacao => Notificacoes.Any();
    public bool FoiInterrompido => Notificacoes.Any(x => x.ProvocaInterrupcaoDoSistema);
    public bool SemInterrupcoes => !FoiInterrompido;

    public abstract void Adicionar(string mensagem, bool exibirParaUsuario, bool requisicaoInvalida, bool provocaInterrupcaoDoSistema);

}
