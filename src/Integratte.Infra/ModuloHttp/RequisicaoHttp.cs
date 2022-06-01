#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Net;

namespace Integratte.Infra.ModuloHttp;

public class RequisicaoHttp
{
    private RequisicaoHttp() { }

    public static RequisicaoHttp Criar(string hostUrl, string rota, MetodoHttpEnum metodoHttp, TokenDaRequisicao? tokenDaRequisicao = null)
    {
        RequisicaoHttp requisicao = new()
        {
            HostUrl = hostUrl,
            Rota = rota,
            MetodoHttp = metodoHttp,
            ITokenDaRequisicao = tokenDaRequisicao
        };

        return requisicao;

    }

    public string HostUrl { get; private set; }
    public string Rota { get; private set; }
    public MetodoHttpEnum MetodoHttp { get; private set; }
    public TokenDaRequisicao? ITokenDaRequisicao { get; private set; }

    public SecurityProtocolType ProtocoloDeSeguranca { get; set; } = SecurityProtocolType.Tls12;
    public string? ProxyUrl { get; set; }
    public int TimeOut { get; set; } = 800000;

    public Dictionary<string, string> ValoresDoCabecalho { get; set; } = new();
    public Dictionary<string, string> ParametrosHttp { get; set; } = new();
    public Dictionary<string, string> ParametrosQueryString { get; set; } = new();
    public object? Conteudo { get; set; }
    public List<ParametroPDF> PDFs { get; set; } = new();

    public enum MetodoHttpEnum
    {
        Get,
        Post,
        Put,
        Delete,
        Patch,

    }

    public class ParametroPDF
    {
        public const string TipoDeConteudoPDF = "application/pdf";

        public ParametroPDF(string nomeDoParametro, string nomeDoArquivo, byte[] bytes)
        {
            NomeDoParametro = nomeDoParametro;
            NomeDoArquivo = nomeDoArquivo;
            Bytes = bytes;

        }

        public string NomeDoParametro { get; private set; }
        public string NomeDoArquivo { get; private set; }
        public byte[] Bytes { get; private set; }

    }

}
