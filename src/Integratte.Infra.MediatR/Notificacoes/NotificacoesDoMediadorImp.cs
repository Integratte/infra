using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.MediatR.Notificacoes;

internal class NotificacoesDoMediadorImp : NotificacoesDoMediador
{
    public override void Adicionar(string mensagem, bool exibirParaUsuario, bool requisicaoInvalida, bool provocaInterrupcaoDoSistema)
    {
        Notificacoes.Add(new Notificacao(mensagem, exibirParaUsuario, requisicaoInvalida, provocaInterrupcaoDoSistema));

    }

}
