using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.ModuloEmails;

public abstract class EnvioDeEmail
{
    protected List<NotificacaoDeEmail> _notificacoes = new();

    public EnvioDeEmail InformarDestinatario(string enderecoDeEmail)
    {
        return InformarDestinatarios(new string[] { enderecoDeEmail });

    }
    public abstract EnvioDeEmail InformarDestinatarios(IEnumerable<string> enderecosDeEmail);
    public abstract EnvioDeEmail InformarAssunto(string assunto);
    public abstract EnvioDeEmail InformarCorpo(string assunto);
    public abstract EnvioDeEmail InformarTokensParaSubstituir(Dictionary<string, string> tokensParaSubstituir);

    public async Task<NotificacaoDeEmail[]> EnviarEmailAsync()
    {
        var retorno = Array.Empty<NotificacaoDeEmail>();

        try
        {
            if (EmailValido())
            {
                SubstituirTokens();

                await TentarEnviarEmail();

            }

        }
        catch (Exception ex) { _notificacoes.Add(new(ex.Message, TipoDeNotificacaoEnum.ErroDoSistema)); }
        finally
        {
            retorno = _notificacoes.ToArray();
            LimparObjeto();

        }

        return retorno;

    }

    protected abstract bool EmailValido();
    protected abstract void SubstituirTokens();
    protected abstract Task TentarEnviarEmail();
    protected abstract void LimparObjeto();

    public class NotificacaoDeEmail
    {
        public string Mensagem { get; private set; }
        public TipoDeNotificacaoEnum TipoDeNotificacaoEnum { get; private set; }

        public NotificacaoDeEmail(string mensagem, TipoDeNotificacaoEnum tipoDeNotificacaoEnum = TipoDeNotificacaoEnum.RequisicaoInvalida)
        {
            Mensagem = mensagem;
            TipoDeNotificacaoEnum = tipoDeNotificacaoEnum;

        }

    }

}
