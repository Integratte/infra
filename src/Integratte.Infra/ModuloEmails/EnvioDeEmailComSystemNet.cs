using Integratte.Infra.ModuloConfiguracoes;
using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Integratte.Infra.ModuloExtensoes;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Integratte.Infra.ModuloEmails;

internal sealed class EnvioDeEmailComSystemNet : EnvioDeEmail
{
    private readonly ConfiguracoesDeEmail _configuracoesDeEmail;
    private readonly IConfiguracoes _configuracoes;

    public EnvioDeEmailComSystemNet(IConfiguracoes configuracoes)
    {
        _configuracoes = configuracoes;
        _configuracoesDeEmail = Carregar_configuracoesDeEmail();

    }

    private ConfiguracoesDeEmail Carregar_configuracoesDeEmail()
    {
        try
        {
            var diretorio = "";
            if (_configuracoes.PastaDeConfiguracoes.ContemValor())
                diretorio = Directory.GetCurrentDirectory() + _configuracoes.PastaDeConfiguracoes;

            var emailSettings = new ConfigurationBuilder().AddJsonFile($"{diretorio}email.settings.json").Build();

            var configuracoes = emailSettings.GetSection("configuracoesDeEmail").Get<ConfiguracoesDeEmail>();
            if (configuracoes == null)
                throw new ErroDeProgramacao("Configurações de e-mail não encontrada.");

            return configuracoes;

        }
        catch (Exception ex)
        {
            throw new ErroDeProgramacao($"Configurações de e-mail não encontrada. Erro: {ex.Message}");

        }

    }

    private List<string> EnderecosDosDestinatarios { get; set; } = new();
    private string Assunto { get; set; } = "";
    private string Corpo { get; set; } = "";
    private Dictionary<string, string> TokensParaSubstituir { get; set; } = new();

    public override EnvioDeEmail InformarDestinatarios(IEnumerable<string> enderecosDeEmail)
    {
        EnderecosDosDestinatarios = enderecosDeEmail.ToList();
        return this;

    }

    public override EnvioDeEmail InformarAssunto(string assunto)
    {
        Assunto = assunto;
        return this;

    }

    public override EnvioDeEmail InformarCorpo(string corpo)
    {
        Corpo = corpo;
        return this;

    }

    public override EnvioDeEmail InformarTokensParaSubstituir(Dictionary<string, string> tokensParaSubstituir)
    {
        if (tokensParaSubstituir == null) return this;

        TokensParaSubstituir = tokensParaSubstituir;
        return this;

    }

    protected override bool EmailValido()
    {
        if (EnderecosDosDestinatarios.Count == 0)
            _notificacoes.Add(new("Necessário informar ao menos um endereço de e-mail."));

        foreach (var email in EnderecosDosDestinatarios)
            if (email.NuloOuVazio())
            {
                _notificacoes.Add(new($"Existem elementos vazios na lista de endereço de e-mail."));
                break;

            }

        if (Assunto.NuloOuVazio())
            _notificacoes.Add(new("Necessário informar o assunto do e-mail."));

        if (Corpo.NuloOuVazio())
            _notificacoes.Add(new("Necessário informar o corpo do e-mail."));

        return _notificacoes.Count == 0;

    }

    protected override void SubstituirTokens()
    {
        foreach (var token in TokensParaSubstituir)
        {
            Assunto = Assunto.Replace(token.Key, token.Value);
            Corpo = Corpo.Replace(token.Key, token.Value);

        }

    }

    protected override async Task TentarEnviarEmail()
    {
        MailMessage mail = new()
        {
            From = new MailAddress(_configuracoesDeEmail.EnderecoDeEnvio, _configuracoesDeEmail.NomeDoEnderecoDeEnvio),
            Subject = Assunto,
            Body = Corpo,
            IsBodyHtml = true,

        };

        foreach (var endereco in EnderecosDosDestinatarios)
            mail.To.Add(endereco);

        using SmtpClient smtp = new(_configuracoesDeEmail.HostSmtp, _configuracoesDeEmail.PortaSmtp);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_configuracoesDeEmail.EnderecoDeEnvio, _configuracoesDeEmail.SenhaDoEnderecoDeEnvio);
        smtp.EnableSsl = _configuracoesDeEmail.SslHabilitado;

        try { await smtp.SendMailAsync(mail); }
        catch (Exception ex)
        {
            var mensagem = $"Tivemos um problema ao enviar o e-mail '{Assunto}'. Erro: {ex.Message}";
            _notificacoes.Add(new(mensagem, TipoDeNotificacaoEnum.ErroDoSistema));

        }

    }

    protected override void LimparObjeto()
    {
        _notificacoes = new();
        EnderecosDosDestinatarios = new();
        Assunto = "";
        Corpo = "";
        TokensParaSubstituir = new();

    }

}
