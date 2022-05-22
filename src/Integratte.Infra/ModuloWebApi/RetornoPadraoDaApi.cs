using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.ModuloWebApi;

public class RetornoPadraoDaApi<T> where T : new()
{
    public RetornoPadraoDaApi(INotificacao[] notificacoes, bool sucedido = false)
    {
        Sucedido = sucedido;
        Notificacoes = notificacoes;

    }

    public RetornoPadraoDaApi(T? resposta, INotificacao[] notificacoes, bool sucedido = true)
    {
        Resposta = resposta;
        Sucedido = sucedido;
        Notificacoes = notificacoes;

    }

    public bool Sucedido { get; private set; }
    public INotificacao[] Notificacoes { get; private set; }
    public T? Resposta { get; set; }

}

public class RetornoPadraoDaApi : RetornoPadraoDaApi<object>
{
    public RetornoPadraoDaApi(INotificacao[] notificacoes, bool sucedido) : base(notificacoes, sucedido) { }

}