using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.MediatR.Notificacoes
{
    internal class Notificacao : INotificacao
    {
        public string Mensagem { get; private set; }
        public bool PodeExibirMensagemParaUsuario { get; private set; }
        public bool RequisicaoInvalida { get; private set; }
        public bool ProvocaInterrupcaoDoSistema { get; private set; }

        public Notificacao(string mensagem, bool podeExibirParaUsuario, bool requisicaoInvalida, bool provocaInterrupcaoDoSistema)
        {
            Mensagem = mensagem;
            PodeExibirMensagemParaUsuario = podeExibirParaUsuario;
            RequisicaoInvalida = requisicaoInvalida;
            ProvocaInterrupcaoDoSistema = provocaInterrupcaoDoSistema;
        }

    }

}
