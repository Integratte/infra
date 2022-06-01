using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.MediatR.Notificacoes
{
    internal class Notificacao : INotificacao
    {
        public string Mensagem { get; private set; }
        public bool PodeExibirParaUsuario { get; private set; }
        public TipoDeNotificacaoEnum TipoDeNotificacaoEnum { get; private set; }
        public string DescricaoDoTipoDeNotificacao => TipoDeNotificacaoEnum.ToString();

        public Notificacao(string mensagem, bool podeExibirParaUsuario, TipoDeNotificacaoEnum tipo)
        {
            Mensagem = mensagem;
            PodeExibirParaUsuario = podeExibirParaUsuario;
            TipoDeNotificacaoEnum = tipo;

        }

    }

}
