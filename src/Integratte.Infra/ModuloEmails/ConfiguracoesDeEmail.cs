namespace Integratte.Infra.ModuloEmails;

internal class ConfiguracoesDeEmail
{
    public string EnderecoDeEnvio { get; set; }
    public string SenhaDoEnderecoDeEnvio { get; set; }
    public string NomeDoEnderecoDeEnvio { get; set; }
    public string HostSmtp { get; set; }
    public int PortaSmtp { get; set; }
    public bool SslHabilitado { get; set; }

}

